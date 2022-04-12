using System;
using CodeBase.Helpers;
using UnityEngine;

namespace CodeBase.Services.InputService
{
    public class InputKeyboard : IInputService
    {
        public event EventHandler Accelerated;
        public event EventHandler<DirectionRotateType> Rotated;
        public event EventHandler ShotBullet;
        public event EventHandler ShotLaser;
        public event EventHandler Pause;
        
        public void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                OnAccelerated();
            }

            var horizontalAxisRaw = (int) Input.GetAxisRaw("Horizontal");
            if (horizontalAxisRaw != 0)
            {
                var directionRotateType = horizontalAxisRaw == -1
                    ? DirectionRotateType.Left
                    : DirectionRotateType.Right;
                
                OnRotated(directionRotateType);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnShotBullet();
            }
            
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                OnShotLaser();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnPause();
            }
        }

        private void OnAccelerated()
        {
            Accelerated?.Invoke(this, EventArgs.Empty);
        }

        private void OnRotated(DirectionRotateType directionRotateType)
        {
            Rotated?.Invoke(this, directionRotateType);
        }

        private void OnShotBullet()
        {
            ShotBullet?.Invoke(this, EventArgs.Empty);
        }

        private void OnPause()
        {
            Pause?.Invoke(this, EventArgs.Empty);
        }

        private void OnShotLaser()
        {
            ShotLaser?.Invoke(this, EventArgs.Empty);
        }
    }
}