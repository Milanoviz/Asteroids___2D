using System;
using CodeBase.Helpers;
using UnityEngine;

namespace CodeBase.BulletModule
{
    public class BulletMover : IBulletMover
    {
        public event EventHandler Destroyed;

        private readonly float _shotDistance;
        private Vector3 _moveDirection;
        private float _lifeTime;
        
        private readonly Transform _bulletTransform;
        
        public BulletMover(Transform bulletTransform)
        {
            _bulletTransform = bulletTransform;
            _shotDistance = CameraSettings.CameraResolution.x * 2;
        }

        public void Update()
        {
            _lifeTime += Time.deltaTime;
            
            _bulletTransform.Translate(_moveDirection * _shotDistance * Time.deltaTime);
            
            CheckShotDistance();
        }

        public void SetMoveDirection(Vector3 direction)
        {
            _lifeTime = 0;
            
            _moveDirection = direction;
        }
        
        private void CheckShotDistance()
        {
            if (_shotDistance < _shotDistance * _lifeTime)
            {
                OnDestroyed();
            }
        }

        private void OnDestroyed()
        {
            Destroyed?.Invoke(this, EventArgs.Empty);
        }
    }
}