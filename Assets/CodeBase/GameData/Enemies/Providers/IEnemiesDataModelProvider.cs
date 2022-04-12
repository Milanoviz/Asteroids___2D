using CodeBase.EnemyModule;
using CodeBase.GameData.Bullets.Model;
using CodeBase.Services;

namespace CodeBase.GameData.Enemies.Providers
{
    public interface IEnemiesDataModelProvider : IService
    {
        float DelayBetweenSpawn { get; }
        EnemyDataModel GetDataModelOfType(EnemiesType type);
    }
}