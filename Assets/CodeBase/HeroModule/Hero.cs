using System;
using System.Collections;
using CodeBase.BulletModule;
using CodeBase.GameData.Hero.Provider;
using CodeBase.Services.InputService;
using UnityEngine;

namespace CodeBase.HeroModule
{
    public class Hero : MonoBehaviour, IHero
    {
        public event EventHandler Hit;

        public Vector2 Position => transform.position;
        public float Angle => _heroMover.Angle;
        public float ActualSpeed => _heroMover.ActualSpeed;
        public int LaserShotCount => _heroArmory.LaserShotCount;
        public float TimeBeforeShotLaser => _heroArmory.TimeBeforeShotLaser;

        [SerializeField] private Transform _shotPoint = default;
        [SerializeField] private LineRenderer _lineRenderer = default;
        
        private const string DefaultLayerName = "Player";
        private const string IgnoreCollisionsLayerName = "Ignore collisions";
        
        private float _delayBetweenRespawn;
        private SpriteRenderer _spriteRenderer;
        
        private IHeroMover _heroMover;
        private IHeroArmory _heroArmory;

        public void Constructor(IInputService inputService, IBulletFactory bulletFactory, IHeroDataModelProvider heroDataModelProvider)
        {
            _delayBetweenRespawn = heroDataModelProvider.HeroDataModels.DelayBetweenRespawn;
            
            var acceleration = heroDataModelProvider.HeroDataModels.Acceleration;
            var rotateSpeed = heroDataModelProvider.HeroDataModels.RotateSpeed;
            
            _heroMover = new HeroMover(transform, inputService, acceleration, rotateSpeed);
            _heroArmory = new HeroArmory(bulletFactory, inputService, _shotPoint, _lineRenderer, heroDataModelProvider);

            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            _heroMover.Update();
            _heroArmory.Update();
        }

        public void Die()
        {
            gameObject.SetActive(false);
        }

        public void Respwan()
        {
            Invoke(nameof(Resurrect), _delayBetweenRespawn);
        }

        private void Resurrect()
        {
            _heroMover.StopMover();

            transform.position = Vector3.zero;
            
            gameObject.layer = LayerMask.NameToLayer(IgnoreCollisionsLayerName);
            gameObject.SetActive(true);
            
            StartCoroutine(Flashing());
            
        }

        private IEnumerator Flashing()
        {
            for (var i = 0; i < _delayBetweenRespawn; i++)
            {
                _spriteRenderer.color = Color.gray;
                yield return new WaitForSeconds(0.5f);
                _spriteRenderer.color = Color.white;
                yield return new WaitForSeconds(0.5f);
            }
            
            gameObject.layer = LayerMask.NameToLayer(DefaultLayerName);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            OnHit();
        }
        
        private void OnHit()
        {
            Hit?.Invoke(this, EventArgs.Empty);
        }
    }
}