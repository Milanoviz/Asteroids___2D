using System;
using UnityEngine;

namespace CodeBase.GameData.AsteroidSpawner.Model
{
    [Serializable]
    public class AsteroidSpawnerDataModel
    {
        [SerializeField]
        private int _startAsteroidsCount;
        [SerializeField]
        private float _splitAngle;
        [SerializeField]
        private int _trajectoryMovementVariance;
        [SerializeField] 
        private int _fragmentsCount;
        [SerializeField] 
        private int _delayBetweenSpawn;

        public int StartAsteroidsCount => _startAsteroidsCount;
        public float SplitAngle => _splitAngle;
        public int TrajectoryMovementVariance => _trajectoryMovementVariance;
        public int FragmentsCount => _fragmentsCount;
        public int DelayBetweenSpawn => _delayBetweenSpawn;
    }
}