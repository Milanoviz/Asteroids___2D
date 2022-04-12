using System.Collections.Generic;
using CodeBase.GameData.Asteroids.Model;
using UnityEngine;

namespace CodeBase.GameData.Asteroids
{
    [CreateAssetMenu(fileName = "AsteroidsConfig", menuName = "Configs/Asteroids", order = 1)]
    public class AsteroidsConfig : ScriptableObject
    {
        [SerializeField] private List<AsteroidDataModel> _asteroidDataModels;

        public List<AsteroidDataModel> AsteroidDataModels => _asteroidDataModels;
    }
}