using UnityEngine;

namespace CodeBase.HeroModule
{
    public interface IHeroMover
    {
        float Angle { get; }
        float ActualSpeed { get; }
        
        void Update();

        void StopMover();
    }
}