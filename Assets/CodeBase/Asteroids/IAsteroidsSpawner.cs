using System;
using CodeBase.Services;

namespace CodeBase.Asteroids
{
    public interface IAsteroidsSpawner : IService
    {
        event EventHandler<int> DestroyedSuccessfully;
    }
}