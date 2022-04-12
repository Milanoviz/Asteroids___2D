using UnityEngine;

namespace CodeBase.Asteroids
{
    public class AsteroidMover : IAsteroidMover
    {
        public Vector3 MoveDirection => _moveDirection;
        
        private Vector3 _moveDirection;

        private readonly Transform _asteroidTransform;
        private readonly float _speed;


        public AsteroidMover(Transform asteroidTransform, float speed)
        {
            _asteroidTransform = asteroidTransform;
            _speed = speed;
        }

        public void Update()
        {
            _asteroidTransform.Translate(_moveDirection * _speed * Time.deltaTime);
        }

        public void SetMoveDirection(Vector3 direction)
        { 
            _moveDirection = direction;
        }
    }
}