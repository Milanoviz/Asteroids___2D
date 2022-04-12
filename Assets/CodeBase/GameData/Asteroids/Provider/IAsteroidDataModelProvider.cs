using System.Collections.Generic;
using CodeBase.GameData.Asteroids.Model;
using CodeBase.Services;

namespace CodeBase.GameData.Asteroids.Provider
{
    public interface IAsteroidDataModelProvider : IService
    {
        List<AsteroidDataModel> AsteroidsDataModels { get; }
    }
}