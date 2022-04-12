using System;
using CodeBase.Services;

namespace CodeBase.EnemyModule
{
    public interface IEnemiesSpawner : IService
    {
        event EventHandler<int> DestroyedSuccessfully;
    }
}