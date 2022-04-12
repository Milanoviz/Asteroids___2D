using System;
using CodeBase.BulletModule;
using CodeBase.GameData.Hero.Provider;
using CodeBase.LaserModule;
using CodeBase.Services.InputService;
using UnityEngine;

namespace CodeBase.HeroModule
{
    public class HeroArmory : IHeroArmory
    {
        public event EventHandler ShotCompleted;
        public int LaserShotCount => _laserShotCount;
        public float TimeBeforeShotLaser => _timeBeforeShotLaser;

        private readonly Transform _shotPoint;
        
        private readonly float _delayBetweenShotBullet;
        private float _timeAfterShotBullet;

        private readonly float _delayBetweenShotLaser;
        private int _laserShotCount;
        private float _timeBeforeShotLaser;
        
        private readonly ILaser _laser;
        private readonly IBulletFactory _bulletFactory;

        public HeroArmory(IBulletFactory bulletFactory, IInputService inputService,
                          Transform shotPoint, LineRenderer lineRenderer, IHeroDataModelProvider dataModelProvider)
        {
            _bulletFactory = bulletFactory;
            _shotPoint = shotPoint;
            _laser = new Laser(shotPoint, lineRenderer, dataModelProvider.HeroDataModels.LaserShotTime);
            
            _delayBetweenShotBullet = dataModelProvider.HeroDataModels.DelayBetweenShotBullet;
            _delayBetweenShotLaser = dataModelProvider.HeroDataModels.DelayBetweenShotLaser;
            _laserShotCount = dataModelProvider.HeroDataModels.LaserShotCount;

            _timeBeforeShotLaser = _delayBetweenShotLaser;
            
            inputService.ShotBullet += HandleShotBullet;
            inputService.ShotLaser += HandleShotLaser;
        }

        public void Update()
        {
            _timeAfterShotBullet += Time.deltaTime;

            _timeBeforeShotLaser -= Time.deltaTime;
            
            _laser.Update();
        }

        private void HandleShotBullet(object sender, EventArgs e)
        {
            ShotBullet();
        }

        private void HandleShotLaser(object sender, EventArgs e)
        {
           ShotLaser();
        }

        private void ShotBullet()
        {
            if (_timeAfterShotBullet > _delayBetweenShotBullet)
            {
                _timeAfterShotBullet = 0;
                
                var bullet = _bulletFactory.CreateBullet(BulletType.Hero, _shotPoint.position);
                bullet.SetMoveDirection(_shotPoint.TransformDirection(Vector3.up));
            }
        }

        private void ShotLaser()
        {
            if (_laserShotCount > 0 && _timeBeforeShotLaser < 0)
            {
                _laser.Activate();
                
                --_laserShotCount;
                _timeBeforeShotLaser = _delayBetweenShotLaser;
            }
        }
    }
}