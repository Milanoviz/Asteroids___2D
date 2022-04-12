using CodeBase.GameData.Hero.Model;
using CodeBase.Services;

namespace CodeBase.GameData.Hero.Provider
{
    public interface IHeroDataModelProvider : IService
    {
       HeroDataModel HeroDataModels { get; }
    }
}