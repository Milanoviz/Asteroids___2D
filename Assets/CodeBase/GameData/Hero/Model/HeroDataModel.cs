using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.GameData.Hero.Model
{
    [Serializable]
    public class HeroDataModel
    {
        [Header("Common")]
        [SerializeField]
        private int _livesCount;
        [SerializeField] 
        private float _delayBetweenRespawn;
        
        [Header("Movement")]
        [SerializeField]
        private float _acceleration;
        [SerializeField]
        private float _rotateSpeed;
        
        [Header("Armory")]
        [SerializeField]
        private float _delayBetweenShotBullet;
        [SerializeField]
        private int _laserShotCount;
        [SerializeField]
        private float laserShotTime;
        [SerializeField]
        private float _delayBetweenShotLaser;

        public int LivesCount => _livesCount;
        public float DelayBetweenRespawn => _delayBetweenRespawn;
        public float Acceleration => _acceleration;
        public float RotateSpeed => _rotateSpeed;
        public float DelayBetweenShotBullet => _delayBetweenShotBullet;
        public int LaserShotCount => _laserShotCount;
        public float LaserShotTime => laserShotTime;
        public float DelayBetweenShotLaser => _delayBetweenShotLaser;
    }
}