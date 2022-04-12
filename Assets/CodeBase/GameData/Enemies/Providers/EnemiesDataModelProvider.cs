using System;
using System.Linq;
using CodeBase.EnemyModule;
using CodeBase.GameData.Bullets.Model;

namespace CodeBase.GameData.Enemies.Providers
{
    public class EnemiesDataModelProvider : IEnemiesDataModelProvider
    {
        public float DelayBetweenSpawn => _config.DelayBetweenSpawn;
        
        private readonly EnemiesConfig _config;
        
        public EnemiesDataModelProvider(EnemiesConfig config)
        {
            _config = config;
        }

        public EnemyDataModel GetDataModelOfType(EnemiesType type)
        {
            var dataModel = _config.EnemiesDataModels.FirstOrDefault(model => model.Type == type);

            if (dataModel == null)
            {
                throw new Exception($"Enemy of this type not found");
            }

            return dataModel;
        }
    }
}