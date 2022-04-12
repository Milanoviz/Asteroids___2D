using CodeBase.Asteroids;
using CodeBase.EnemyModule;
using CodeBase.Helpers;
using UnityEngine;

namespace CodeBase.LaserModule
{
    public class Laser : ILaser
    {
        private readonly float _shotTime;
        private float _timeAfterShot;

        private bool _isActive;

        private readonly Transform _shotPoint;
        private readonly LineRenderer _lineRenderer;

        public Laser(Transform shotPoint, LineRenderer lineRenderer, float shotTime)
        {
            _shotPoint = shotPoint;
            _lineRenderer = lineRenderer;
            _shotTime = shotTime;
        }

        public void Activate()
        {
            _isActive = true;
        }

        public void Update()
        {
            if (_isActive)
            {
                if (_shotTime > _timeAfterShot)
                {
                    Shot();
                    _timeAfterShot += Time.deltaTime;
                }
                else
                {
                    _isActive = false;
                    _timeAfterShot = 0;
                    DrawLaser(Vector3.zero, Vector3.zero);
                }
            }
        }

        private void DrawLaser(Vector3 startPoint, Vector3 endPoint)
        {
            _lineRenderer.SetPosition(0, startPoint);
            _lineRenderer.SetPosition(1, endPoint);
        }

        private void Shot()
        {
            var startPoint = _shotPoint.position;
            var direction = _shotPoint.up;

            DrawLaser(startPoint, direction * CameraSettings.CameraResolution.x);
            
            if (Physics2D.Raycast(startPoint, direction))
            {
                HandleHits(startPoint, direction);
            }
        }

        private void HandleHits(Vector3 startPoint, Vector3 direction)
        {
            var hits = Physics2D.RaycastAll(startPoint, direction);

            foreach (var item in hits)
            {
                if (item.collider.gameObject.TryGetComponent(out Asteroid asteroid))
                {
                    asteroid.DestroyCompletely();
                }
                    
                if (item.collider.gameObject.TryGetComponent(out Enemy enemy))
                {
                    enemy.DestroyCompletely();
                }
            }
        }
    }
}