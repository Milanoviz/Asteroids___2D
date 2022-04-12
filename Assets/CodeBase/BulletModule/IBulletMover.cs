using System;
using UnityEngine;

namespace CodeBase.BulletModule
{
    public interface IBulletMover
    {
        event EventHandler Destroyed;
        
        void Update();

        void SetMoveDirection(Vector3 direction);
    }
}