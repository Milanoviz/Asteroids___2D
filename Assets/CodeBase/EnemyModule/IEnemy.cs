using System;
using UnityEngine;

namespace CodeBase.EnemyModule
{
    public interface IEnemy
    {
        event EventHandler Destroyed;
        
        int Score { get; }
        
        void Constructor(Transform target, float speed, int score);
    }
}