using System.Collections.Generic;
using CodeBase.GameData.Bullets.Model;
using UnityEngine;

namespace CodeBase.GameData.Enemies
{
    [CreateAssetMenu(fileName = "EnemiesConfig", menuName = "Configs/Enemies", order = 1)]
    public class EnemiesConfig : ScriptableObject
    {
        [SerializeField] private float _delayBetweenSpawn;
        [SerializeField] private List<EnemyDataModel> _enemiesDataModels;

        public float DelayBetweenSpawn => _delayBetweenSpawn;
        public List<EnemyDataModel> EnemiesDataModels => _enemiesDataModels;
    }
}