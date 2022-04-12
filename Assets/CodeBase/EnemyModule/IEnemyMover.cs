using UnityEngine;

namespace CodeBase.EnemyModule
{
    public interface IEnemyMover
    {
        void Update();
    }

    public class EnemyMover : IEnemyMover
    {
        private float _speed;
        
        private readonly Transform _enemyTransform;
        private readonly Transform _target;

        public EnemyMover(float speed, Transform enemyTransform, Transform target)
        {
            _speed = speed;
            _enemyTransform = enemyTransform;
            _target = target;
        }

        public void Update()
        { 
            _enemyTransform.position = Vector2.MoveTowards(_enemyTransform.position, _target.position, _speed * Time.deltaTime);
        }
    }
}