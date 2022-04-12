using CodeBase.Services;
using UnityEngine;

namespace CodeBase.EnemyModule
{
    public interface IEnemyFactory : IService
    {
        IEnemy CreateEnemyOfType(EnemiesType type, Vector3 spawnPoint);

        void DestroyEnemy(Enemy enemy);
    }
}