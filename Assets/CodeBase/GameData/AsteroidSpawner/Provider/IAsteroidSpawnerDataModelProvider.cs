using CodeBase.GameData.AsteroidSpawner.Model;
using CodeBase.Services;

namespace CodeBase.GameData.AsteroidSpawner.Provider
{
    public interface IAsteroidSpawnerDataModelProvider : IService
    {
        AsteroidSpawnerDataModel AsteroidsDataModel { get; }
    }
}