using CodeBase.BulletModule;
using CodeBase.GameData.Bullets.Model;
using CodeBase.Services;

namespace CodeBase.GameData.Bullets.Provider
{
    public interface IBulletDataModelProvider : IService
    {
        BulletDataModel GetDataModelOfType(BulletType type);
    }
}