using UnityEngine;

namespace CodeBase.Asteroids
{
    public interface IAsteroidMover
    {
        Vector3 MoveDirection { get; }
        
        void Update();

        void SetMoveDirection(Vector3 direction);
    }
}