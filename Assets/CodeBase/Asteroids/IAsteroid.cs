using System;
using CodeBase.Asteroids.Args;
using UnityEngine;

namespace CodeBase.Asteroids
{
    public interface IAsteroid
    {
        event EventHandler<CollidingAsteroidArgs> Hit;
        event EventHandler DestroyedCompletely;
        
        int Score { get; }
        
        void Constructor(float speed, int score);
        
        void SetMoveDirection(Vector3 direction);
    }
}