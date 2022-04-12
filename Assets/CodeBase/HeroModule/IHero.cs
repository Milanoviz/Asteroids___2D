using System;
using CodeBase.BulletModule;
using CodeBase.GameData.Hero.Provider;
using CodeBase.Services.InputService;
using UnityEngine;

namespace CodeBase.HeroModule
{
    public interface IHero
    {
        event EventHandler Hit;
        
        Vector2 Position { get; }
        float Angle { get; }
        float ActualSpeed { get; }
        int LaserShotCount { get; }
        float TimeBeforeShotLaser { get; }

        void Constructor(IInputService inputService, IBulletFactory bulletFactory, IHeroDataModelProvider heroDataModelProvider);

        void Die();
        void Respwan();
    }
}