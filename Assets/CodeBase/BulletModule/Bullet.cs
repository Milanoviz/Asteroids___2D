using System;
using UnityEngine;

namespace CodeBase.BulletModule
{
    public class Bullet : MonoBehaviour, IBullet
    {
        public event EventHandler Destroyed;
        
        public IBulletMover BulletMover => _bulletMover;

        private IBulletMover _bulletMover;
        
        public void Constructor()
        {
            _bulletMover = new BulletMover(transform);
            
            _bulletMover.Destroyed += HandlerDestroyed;
        }

        private void Update()
        {
            _bulletMover.Update();
        }

        public void SetMoveDirection(Vector3 direction)
        {
            _bulletMover.SetMoveDirection(direction);
        }

        private void HandlerDestroyed(object sender, EventArgs e)
        {
            OnDestroyed();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            OnDestroyed();
        }

        private void OnDestroyed()
        {
            Destroyed?.Invoke(this, EventArgs.Empty);
        }
    }
}