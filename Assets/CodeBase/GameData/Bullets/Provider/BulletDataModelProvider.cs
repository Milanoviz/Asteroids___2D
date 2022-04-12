using System;
using System.Linq;
using CodeBase.BulletModule;
using CodeBase.GameData.Bullets.Model;

namespace CodeBase.GameData.Bullets.Provider
{
    public class BulletDataModelProvider : IBulletDataModelProvider
    {
        private readonly BulletsConfig _config;

        public BulletDataModelProvider(BulletsConfig config)
        {
            _config = config;
        }
        
        public BulletDataModel GetDataModelOfType(BulletType type)
        {
            var dataModel = _config.BulletsDataModels.FirstOrDefault(model => model.Type == type);

            if (dataModel == null)
            {
                throw new Exception($"Bullets of this type not found");
            }

            return dataModel;
        }
    }
}