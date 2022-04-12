using System.Collections.Generic;
using CodeBase.GameData.Asteroids.Model;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Asteroids
{
    public interface IAsteroidsFactory : IService
    {
        List<IAsteroid> ActiveAsteroids { get; }

        Asteroid CreateDefaultAsteroid(Vector3 spawnPoint);
        Asteroid CreateAsteroid(int asteroidIndex, Vector3 spawnPoint);
        void DestroyAsteroid(Asteroid asteroid);

        int GetIndexAsteroid(Asteroid asteroid);
    }
}