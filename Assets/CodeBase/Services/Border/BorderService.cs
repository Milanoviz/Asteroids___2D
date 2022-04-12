using System;
using System.Collections.Generic;
using CodeBase.Asteroids;
using CodeBase.Helpers;
using CodeBase.Services.TickerService;
using UnityEngine;

namespace CodeBase.Services.Border
{
    public class BorderService : IBorderService
    {
        public event EventHandler<Transform> ChangePositionX;
        public event EventHandler<Transform> ChangePositionY;

        private readonly Plane[] _planes;
        private readonly List<Transform> _traceableItems;

        public BorderService(ITicker ticker)
        {
            _planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

            _traceableItems = new List<Transform>();
            
            ticker.Updated += HandleUpdated;
        }

        private void HandleUpdated(object sender, EventArgs e)
        {
            CheckItems();
        }

        public void AddTrackedItem(Transform item)
        {
            _traceableItems.Add(item);
        }

        public void RemoveTrackedItem(Transform item)
        {
            _traceableItems.Remove(item);
        }
        
        private void CheckItems()
        {
            if (_traceableItems.Count > 0)
            {
                foreach (var item in _traceableItems)
                {
                    if (item.gameObject.TryGetComponent(out Asteroid asteroid))
                    {
                        if (!GeometryUtility.TestPlanesAABB(_planes, asteroid.SpriteRenderer.bounds))
                        {
                            CheckExitBorder(item);
                        }
                    }
                    else
                    {
                        CheckExitBorder(item);
                    }
                }
            }
        }

        private void CheckExitBorder(Transform item)
        {
            if (Mathf.Abs(item.position.x) > Mathf.Abs(CameraSettings.CameraResolution.x))
            {
                OnChangePositionX(item);
            }

            if (Mathf.Abs(item.position.y) > Mathf.Abs(CameraSettings.CameraResolution.y))
            {
                OnChangePositionY(item);
            }
        }

        private void OnChangePositionX(Transform item)
        {
            ChangePositionX?.Invoke(this, item);
        }
        
        private void OnChangePositionY(Transform item)
        {
            ChangePositionY?.Invoke(this, item);
        }
    }
}