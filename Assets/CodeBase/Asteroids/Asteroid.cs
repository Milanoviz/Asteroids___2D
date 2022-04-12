using System;
using CodeBase.Asteroids.Args;
using UnityEngine;

namespace CodeBase.Asteroids
{
    public class Asteroid : MonoBehaviour, IAsteroid
    {
        public event EventHandler<CollidingAsteroidArgs> Hit;
        public event EventHandler DestroyedCompletely;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public int Score => _score;
        
        private Vector3 _moveDirection => _asteroidMover.MoveDirection;

        private int _score;
        private SpriteRenderer _spriteRenderer;
        private IAsteroidMover _asteroidMover;


        public void Constructor(float speed, int score)
        {
            _asteroidMover = new AsteroidMover(transform, speed);
            _score = score;
            
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetMoveDirection(Vector3 direction)
        {
            _asteroidMover.SetMoveDirection(direction);
        }

        public void DestroyCompletely()
        {
            OnDestroyedCompletely();
        }

        private void Update()
        {
            _asteroidMover.Update();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            OnHit(_moveDirection, col.gameObject);
        }

        private void OnHit(Vector3 moveDirection, GameObject collidingObject)
        {
            var collidingAsteroidArgs = new CollidingAsteroidArgs(moveDirection, collidingObject);
            
            Hit?.Invoke(this, collidingAsteroidArgs);
        }

        private void OnDestroyedCompletely()
        {
            DestroyedCompletely?.Invoke(this, EventArgs.Empty);
        }
    }
}