using System;
using UnityEngine;

namespace CodeBase.EnemyModule
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        public event EventHandler Destroyed;
        
        public int Score => _score;
        
        private int _score;
        private IEnemyMover _enemyMover;

        public void Constructor(Transform target, float speed, int score)
        {
            _enemyMover = new EnemyMover(speed, enemyTransform: transform, target);
            _score = score;
        }

        private void Update()
        {
            if (_enemyMover != null)
            {
                _enemyMover.Update();
            }
        }

        public void DestroyCompletely()
        {
            OnDestroyed();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            OnDestroyed();
        }

        private void OnDestroyed()
        {
            Destroyed?.Invoke(this, EventArgs.Empty);
        }
    }
}