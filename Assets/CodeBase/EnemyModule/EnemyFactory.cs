using CodeBase.GameData.Enemies.Providers;
using CodeBase.HeroModule;
using CodeBase.Services.Border;
using CodeBase.Services.Pool;
using UnityEngine;

namespace CodeBase.EnemyModule
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IPoolManager _poolManager;
        private readonly IBorderService _borderService;
        private readonly IEnemiesDataModelProvider _dataModelProvider;
        private readonly Hero _hero;

        public EnemyFactory(IPoolManager poolManager, IBorderService borderService,
                            IEnemiesDataModelProvider dataModelProvider, Hero hero)
        {
            _poolManager = poolManager;
            _borderService = borderService;
            _dataModelProvider = dataModelProvider;
            _hero = hero;
        }

        public IEnemy CreateEnemyOfType(EnemiesType type, Vector3 spawnPoint)
        {
            var dataModel = _dataModelProvider.GetDataModelOfType(type);

            var enemy = _poolManager.GetGameObject(dataModel.Path).GetComponent<Enemy>();
            enemy.transform.position = spawnPoint;
            
            if (enemy.Score == 0)
            {
                enemy.Constructor(_hero.transform, dataModel.Speed, dataModel.Score);
            }
            
            _borderService.AddTrackedItem(enemy.transform);
            
            return enemy;
        }
        
        public void DestroyEnemy(Enemy enemy)
        {
            _poolManager.PutGameObjectToPool(enemy.gameObject);
            _borderService.RemoveTrackedItem(enemy.transform);
            
        }
    }
}