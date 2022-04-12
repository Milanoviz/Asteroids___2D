using System;
using CodeBase.GameData.Bullets.Provider;
using CodeBase.Services.Border;
using CodeBase.Services.Pool;
using UnityEngine;

namespace CodeBase.BulletModule
{
    public class BulletFactory : IBulletFactory
    {
        private readonly IPoolManager _poolManager;
        private readonly IBorderService _borderService;
        private readonly IBulletDataModelProvider _dataModelProvider;

        public BulletFactory(IPoolManager poolManager, IBorderService borderService,
                             IBulletDataModelProvider dataModelProvider)
        {
            _poolManager = poolManager;
            _borderService = borderService;
            _dataModelProvider = dataModelProvider;
        }


        public IBullet CreateBullet(BulletType bulletType, Vector3 shotPoint)
        {
            var dataModel = _dataModelProvider.GetDataModelOfType(bulletType);

            var bullet = _poolManager.GetGameObject(dataModel.Path).GetComponent<Bullet>();
            bullet.transform.position = shotPoint;
            
            if (bullet.BulletMover == null)
            {
                bullet.Constructor();
            }
            
            _borderService.AddTrackedItem(bullet.transform);
            
            bullet.Destroyed += HandlerBulletDestroyed;

            return bullet;
        }

        public void DestroyBullet(Bullet bullet)
        {
            _poolManager.PutGameObjectToPool(bullet.gameObject);
            _borderService.RemoveTrackedItem(bullet.transform);
            bullet.Destroyed -= HandlerBulletDestroyed;
        }

        private void HandlerBulletDestroyed(object sender, EventArgs e)
        {
            var bullet = sender as Bullet;
            
            DestroyBullet(bullet);
        }
    }
}