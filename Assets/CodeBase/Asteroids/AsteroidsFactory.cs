using System.Collections.Generic;
using System.Linq;
using CodeBase.GameData.Asteroids.Provider;
using CodeBase.Services.Pool;
using UnityEngine;

namespace CodeBase.Asteroids
{
    public class AsteroidsFactory : IAsteroidsFactory
    {
        public List<IAsteroid> ActiveAsteroids => _activeAsteroids;

        private List<IAsteroid> _activeAsteroids;
        
        private readonly IPoolManager _poolManager;
        private readonly IAsteroidDataModelProvider _dataProvider;

        public AsteroidsFactory(IPoolManager poolManager, IAsteroidDataModelProvider dataProvider)
        {
            _poolManager = poolManager;
            _dataProvider = dataProvider;

            _activeAsteroids = new List<IAsteroid>();
        }

        public Asteroid CreateAsteroid(int asteroidIndex, Vector3 spawnPoint)
        {
            var asteroidDataModel = _dataProvider.AsteroidsDataModels[asteroidIndex];
            var asteroid = _poolManager.GetGameObject(asteroidDataModel.Path).GetComponent<Asteroid>();
            
            if (asteroid.Score == 0)
            {
                asteroid.Constructor(asteroidDataModel.Speed, asteroidDataModel.Score);
            }
            
            asteroid.transform.position = spawnPoint;
            
            _activeAsteroids.Add(asteroid);

            return asteroid;
        }

        public Asteroid CreateDefaultAsteroid(Vector3 spawnPoint)
        {
            var defaultAsteroidDataModel = _dataProvider.AsteroidsDataModels.Last();
            
            var asteroid = _poolManager.GetGameObject(defaultAsteroidDataModel.Path).GetComponent<Asteroid>();
            
            if (asteroid.Score == 0)
            {
                asteroid.Constructor(defaultAsteroidDataModel.Speed, defaultAsteroidDataModel.Score);
            }
            
            asteroid.transform.position = spawnPoint;
            
            _activeAsteroids.Add(asteroid);

            return asteroid;
        }

        public void DestroyAsteroid(Asteroid asteroid)
        {
            _poolManager.PutGameObjectToPool(asteroid.gameObject);
            _activeAsteroids.Remove(asteroid);
        }

        public int GetIndexAsteroid(Asteroid asteroid)
        {
            var asteroidScore = asteroid.Score;

            var dataModel = _dataProvider.AsteroidsDataModels.FirstOrDefault(model => model.Score == asteroidScore);

            var index = _dataProvider.AsteroidsDataModels.IndexOf(dataModel);

            return index;
        }
    }
}