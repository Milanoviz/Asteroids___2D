using System;
using CodeBase.Helpers;
using CodeBase.Services.InputService;
using UnityEngine;

namespace CodeBase.HeroModule
{
    public class HeroMover : IHeroMover
    {
        public float Angle => GetAngle();
        public float ActualSpeed => GetActualSpeed();
        
        private readonly float _acceleration;
        private readonly float _rotateSpeed;
        
        private Vector3 _moveDirection;

        private readonly Transform _heroTransform;
        private readonly IInputService _inputService;

        public HeroMover(Transform heroTransform, IInputService inputService, float acceleration, float rotateSpeed)
        {
            _heroTransform = heroTransform;
            _inputService = inputService;
            _acceleration = acceleration;
            _rotateSpeed = rotateSpeed;

            _inputService.Accelerated += HandleAccelerated;
            _inputService.Rotated += HandleRotated;
        }

        public void Update()
        {
            _inputService.Update();
            
            _heroTransform.position += _moveDirection;
        }

        public void StopMover()
        {
            _moveDirection = Vector3.zero;
        }
        
        private float GetActualSpeed()
        {
            var position = _heroTransform.position;
            var result = Vector2.Distance(position, position + _moveDirection)/ Time.deltaTime;
            return result;
        }
        private float GetAngle()
        {
            var transformDirection = _heroTransform.TransformDirection(Vector2.up);
            
            var angle = Vector2.Angle(transformDirection, Vector2.up);
            return angle;
        }

        private void HandleAccelerated(object sender, EventArgs e)
        {
            Acceleration();
        }

        private void HandleRotated(object sender, DirectionRotateType direction)
        {
            Rotation(direction);
        }
        
        private void Acceleration()
        {
            var playerDirection = _heroTransform.TransformDirection(Vector3.up);
            _moveDirection += playerDirection * _acceleration * Time.deltaTime;
        }

        private void Rotation(DirectionRotateType directionRotateType)
        {
            _heroTransform.Rotate(Vector3.forward, GetAngle(directionRotateType));
        }
        
        private float GetAngle(DirectionRotateType directionRotateType)
        {
            var sign = 0;
        
            switch (directionRotateType)
            {
                case DirectionRotateType.Left:
                    sign = 1;
                    break;
                case DirectionRotateType.Right:
                    sign = -1;
                    break;
            }
            return sign * _rotateSpeed * Time.deltaTime;
        }
    }
}