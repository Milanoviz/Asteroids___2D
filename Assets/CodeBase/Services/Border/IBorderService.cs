using System;
using UnityEngine;

namespace CodeBase.Services.Border
{
    public interface IBorderService : IService
    {
        event EventHandler<Transform> ChangePositionX;
        event EventHandler<Transform> ChangePositionY;
        
        void AddTrackedItem(Transform item);
        void RemoveTrackedItem(Transform item);
    }
}