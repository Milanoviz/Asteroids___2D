using CodeBase.Services;
using UnityEngine;

namespace CodeBase.BulletModule
{
    public interface IBulletFactory : IService
    {
        IBullet CreateBullet(BulletType bulletType, Vector3 shotPoint);
        void DestroyBullet(Bullet bullet);
    }
}