using System;
using System.Collections.Generic;
using CodeBase.Helpers;
using CodeBase.Services.TickerService;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.EnemyModule
{
    public class EnemiesSpawner : IEnemiesSpawner
    {
        public event EventHandler<int> DestroyedSuccessfully;

        private float _delayBetweenSpawn;
        private float _timeToSpawn;
        
        private readonly List<float> _spawnPositionX;
        private readonly Vector3 _cameraResolution;

        private readonly IEnemyFactory _enemyFactory;

        public EnemiesSpawner(IEnemyFactory enemyFactory, float delayBetweenSpawn, ITicker ticker)
        {
            _enemyFactory = enemyFactory;
            _delayBetweenSpawn = delayBetweenSpawn;

            _cameraResolution = CameraSettings.CameraResolution;
            
            _spawnPositionX = new List<float> { _cameraResolution.x, -_cameraResolution.x };
            
            ticker.Updated += HandlerUpdate;
        }

        private void Update()
        {
            _timeToSpawn += Time.deltaTime;

            TrySpawn();
        }

        private void TrySpawn()
        {
            if (_timeToSpawn > _delayBetweenSpawn)
            {
                _timeToSpawn = 0;
                Spawn();
            }
        }

        private void Spawn()
        {
            var spawnPoint = new Vector3(_spawnPositionX[Random.Range(0,_spawnPositionX.Count)],
                                       Random.Range(-_cameraResolution.y, _cameraResolution.y));

            var enemy = _enemyFactory.CreateEnemyOfType(EnemiesType.UFO, spawnPoint);
            
            enemy.Destroyed += HandlerEnemyOnDestroyed;
        }

        private void HandlerEnemyOnDestroyed(object sender, EventArgs e)
        {
            var enemy = sender as Enemy;

            if (enemy != null)
            {
                _enemyFactory.DestroyEnemy(enemy);
                enemy.Destroyed -= HandlerEnemyOnDestroyed;
                
                OnDestroyedSuccessfully(enemy.Score);
            }
        }
        
        private void HandlerUpdate(object sender, EventArgs e)
        {
            Update();
        }

        private void OnDestroyedSuccessfully(int score)
        {
            DestroyedSuccessfully?.Invoke(this, score);
        }
    }
}