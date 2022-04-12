using CodeBase.GameData.Hero.Model;

namespace CodeBase.GameData.Hero.Provider
{
    public class HeroDataModelProvider : IHeroDataModelProvider
    {
        public HeroDataModel HeroDataModels => _heroConfig.HeroDataModel;

        private readonly HeroConfig _heroConfig;
        
        public HeroDataModelProvider(HeroConfig heroConfig)
        {
            _heroConfig = heroConfig;
        }

        
    }
}