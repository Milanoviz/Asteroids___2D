using System;
using UnityEngine;

namespace CodeBase.Asteroids.Args
{
    public class CollidingAsteroidArgs : EventArgs
    {
        public Vector3 MoveDirection { get; }

        public GameObject CollidingObject { get; }

        public CollidingAsteroidArgs(Vector3 moveDirection, GameObject collidingObject)
        {
            MoveDirection = moveDirection;
            CollidingObject = collidingObject;
        }
    }
}