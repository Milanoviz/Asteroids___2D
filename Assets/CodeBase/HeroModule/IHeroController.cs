using UnityEngine;

namespace CodeBase.HeroModule
{
    public interface IHeroController
    {
        Vector2 Position { get; }
        float Angle { get; }
        float ActualSpeed { get; }
        int LaserShotCount { get; }
        float TimeBeforeShotLaser { get; }
    }
}