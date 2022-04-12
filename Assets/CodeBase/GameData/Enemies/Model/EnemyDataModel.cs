using System;
using CodeBase.EnemyModule;
using UnityEngine;

namespace CodeBase.GameData.Bullets.Model
{
    [Serializable]
    public class EnemyDataModel
    {
        [SerializeField]
        private EnemiesType _type;
        [SerializeField]
        private string _path;
        [SerializeField]
        private float _speed;
        [SerializeField]
        private int _score;
        
        public EnemiesType Type => _type;
        public string Path => _path;
        public float Speed => _speed;
        public int Score => _score;
    }
}