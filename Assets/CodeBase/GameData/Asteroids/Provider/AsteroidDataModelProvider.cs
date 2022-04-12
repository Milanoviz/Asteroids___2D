using System.Collections.Generic;
using CodeBase.GameData.Asteroids.Model;

namespace CodeBase.GameData.Asteroids.Provider
{
    public class AsteroidDataModelProvider : IAsteroidDataModelProvider
    {
        public List<AsteroidDataModel> AsteroidsDataModels => _asteroidsConfig.AsteroidDataModels;

        private readonly AsteroidsConfig _asteroidsConfig;
        
        public AsteroidDataModelProvider(AsteroidsConfig asteroidsConfig)
        {
            _asteroidsConfig = asteroidsConfig;
        }
    }
}