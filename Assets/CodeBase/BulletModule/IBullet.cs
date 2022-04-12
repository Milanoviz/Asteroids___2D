using System;
using UnityEngine;

namespace CodeBase.BulletModule
{
    public interface IBullet
    {
        event EventHandler Destroyed;
        
        IBulletMover BulletMover { get; }
        
        void Constructor();

        void SetMoveDirection(Vector3 direction);
    }
}