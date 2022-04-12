using System;
using System.Collections.Generic;
using CodeBase.Asteroids.Args;
using CodeBase.GameData.AsteroidSpawner.Provider;
using CodeBase.Helpers;
using CodeBase.HeroModule;
using CodeBase.Services.Border;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Asteroids
{
    public class AsteroidsSpawner : IAsteroidsSpawner
    { 
        public event EventHandler<int> DestroyedSuccessfully;
        
        private int _spawnAmount;
        private float _splitAngle;
        private float _trajectoryMovementVariance;
        private int _fragmentCount;
        private float _delayBetweenSpawn;

        private List<IAsteroid> _activeAsteroids => _asteroidsFactory.ActiveAsteroids;
        
        private readonly Vector3 _cameraResolution;
        private readonly List<float> _spawnPositionY;

        private readonly IBorderService _borderService;
        private readonly IAsteroidsFactory _asteroidsFactory;

        public AsteroidsSpawner(IAsteroidsFactory asteroidsFactory, IBorderService borderService,
                                IAsteroidSpawnerDataModelProvider asteroidSpawnerDataModelProvider)
        {
            _asteroidsFactory = asteroidsFactory;
            _borderService = borderService;

            _cameraResolution = CameraSettings.CameraResolution;
            _spawnPositionY = new List<float> { _cameraResolution.y, -_cameraResolution.y };

            Initialize(asteroidSpawnerDataModelProvider);
            Spawn();
        }

        private void Initialize(IAsteroidSpawnerDataModelProvider dataModelProvider)
        {
            _spawnAmount = dataModelProvider.AsteroidsDataModel.StartAsteroidsCount;
            _splitAngle = dataModelProvider.AsteroidsDataModel.SplitAngle;
            _trajectoryMovementVariance = dataModelProvider.AsteroidsDataModel.TrajectoryMovementVariance;
            _fragmentCount = dataModelProvider.AsteroidsDataModel.FragmentsCount;
            _delayBetweenSpawn = dataModelProvider.AsteroidsDataModel.DelayBetweenSpawn;
        }

        private void Spawn()
        {
            for (var i = 0; i < _spawnAmount; i++)
            {
                var spawnPoint = new Vector3(Random.Range(-_cameraResolution.x, _cameraResolution.x), _spawnPositionY[Random.Range(0,_spawnPositionY.Count)]);
                var spawnDirection = Vector3.zero - spawnPoint;
                var variance = Random.Range(-_trajectoryMovementVariance, _trajectoryMovementVariance);
                var rotation = Quaternion.AngleAxis(variance, Vector3.forward);

                var asteroid = _asteroidsFactory.CreateDefaultAsteroid(spawnPoint);
                asteroid.SetMoveDirection(rotation * spawnDirection);
                
                _borderService.AddTrackedItem(asteroid.transform);
                asteroid.Hit += HandlerHit;
                asteroid.DestroyedCompletely += HandlerDestroyedCompletely;
            }
            
            ++_spawnAmount;
        }

        private void HandlerHit(object sender, CollidingAsteroidArgs args)
        {
            var asteroid = sender as Asteroid;
            
            if (asteroid != null)
            {
                _asteroidsFactory.DestroyAsteroid(asteroid);
                _borderService.RemoveTrackedItem(asteroid.transform);
                asteroid.Hit -= HandlerHit;
                asteroid.DestroyedCompletely -= HandlerDestroyedCompletely;

                var asteroidIndex = _asteroidsFactory.GetIndexAsteroid(asteroid);
            
                if (HasSplit(args.CollidingObject, asteroidIndex))
                {
                    var newAsteroidsIndex = asteroidIndex - 1;
                    Split(newAsteroidsIndex, asteroid.transform.position, args.MoveDirection);
                }
                
                OnDestroyedSuccessfully(asteroid.Score);
            }

            if (_activeAsteroids.Count <= 0)
            {
                Spawn();
            }
        }

        private void HandlerDestroyedCompletely(object sender, EventArgs e)
        {
            var asteroid = sender as Asteroid;
            
            if (asteroid != null)
            {
                _asteroidsFactory.DestroyAsteroid(asteroid);
                _borderService.RemoveTrackedItem(asteroid.transform);
                asteroid.Hit -= HandlerHit;
                asteroid.DestroyedCompletely -= HandlerDestroyedCompletely;
                
                OnDestroyedSuccessfully(asteroid.Score);
            }
            
            if (_activeAsteroids.Count <= 0)
            {
                Spawn();
            }
        }

        private void Split(int asteroidIndex, Vector3 spawnPoint, Vector3 moveDirection)
        {
            var fragments = new List<Asteroid>();
            
            for (var i = 0; i < _fragmentCount; i++)
            {
                var asteroid = _asteroidsFactory.CreateAsteroid(asteroidIndex, spawnPoint);
                _borderService.AddTrackedItem(asteroid.transform);
                asteroid.Hit += HandlerHit;
                asteroid.DestroyedCompletely += HandlerDestroyedCompletely;
                
                fragments.Add(asteroid);
            }
            
            fragments[0].SetMoveDirection(Quaternion.AngleAxis(_splitAngle, Vector3.forward) * moveDirection);
            fragments[1].SetMoveDirection(Quaternion.AngleAxis(-_splitAngle, Vector3.forward) * moveDirection);
        }

        private bool HasSplit(GameObject collidingObject, int asteroidIndex)
        {
            return !collidingObject.TryGetComponent(out Hero hero) && asteroidIndex > 0;
        }

        private  void OnDestroyedSuccessfully(int score)
        {
            DestroyedSuccessfully?.Invoke(this, score);
        }
    }
}