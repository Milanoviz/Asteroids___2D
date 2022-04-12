using CodeBase.GameData.AsteroidSpawner.Model;

namespace CodeBase.GameData.AsteroidSpawner.Provider
{
    public class AsteroidSpawnerDataModelProvider : IAsteroidSpawnerDataModelProvider
    {
        public AsteroidSpawnerDataModel AsteroidsDataModel => _asteroidSpawnerConfig.AsteroidSpawnerDataModel;
        
        private readonly AsteroidSpawnerConfig _asteroidSpawnerConfig;
        
        public AsteroidSpawnerDataModelProvider(AsteroidSpawnerConfig asteroidSpawnerConfig)
        {
            _asteroidSpawnerConfig = asteroidSpawnerConfig;
        }
    }
}