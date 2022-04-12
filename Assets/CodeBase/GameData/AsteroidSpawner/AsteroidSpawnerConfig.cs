using CodeBase.GameData.AsteroidSpawner.Model;
using UnityEngine;

namespace CodeBase.GameData.AsteroidSpawner
{
    [CreateAssetMenu(fileName = "AsteroidSpawnerConfig", menuName = "Configs/AsteroidSpawner", order = 1)]
    public class AsteroidSpawnerConfig : ScriptableObject
    {
        [SerializeField] private AsteroidSpawnerDataModel _asteroidSpawnerDataModel;

        public AsteroidSpawnerDataModel AsteroidSpawnerDataModel => _asteroidSpawnerDataModel;
    }
}