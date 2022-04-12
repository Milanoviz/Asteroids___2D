using System;

namespace CodeBase.HeroModule
{
    public interface IHeroArmory
    {
        event EventHandler ShotCompleted;
        int LaserShotCount { get;  }
        float TimeBeforeShotLaser { get;  }

        void Update();
    }
}