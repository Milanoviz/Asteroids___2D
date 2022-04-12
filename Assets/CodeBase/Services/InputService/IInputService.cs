using System;
using CodeBase.Helpers;

namespace CodeBase.Services.InputService
{
    public interface IInputService :  IService
    {
        event EventHandler Accelerated;
        event EventHandler<DirectionRotateType> Rotated;
        event EventHandler ShotBullet;
        event EventHandler ShotLaser;
        event EventHandler Pause;

        void Update();
    }
}