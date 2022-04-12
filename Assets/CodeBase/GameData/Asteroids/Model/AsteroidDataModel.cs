using System;
using UnityEngine;

namespace CodeBase.GameData.Asteroids.Model
{
    [Serializable]
    public class AsteroidDataModel
    {
        [SerializeField]
        private string _path;
        [SerializeField]
        private float _speed;
        [SerializeField]
        private int _score;

        public string Path => _path;
        public float Speed => _speed;
        public int Score => _score;
    }
}