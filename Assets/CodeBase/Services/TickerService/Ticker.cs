using System;
using UnityEngine;

namespace CodeBase.Services.TickerService
{
    public class Ticker : MonoBehaviour, ITicker
    {
        public event EventHandler Updated;

        private void Update()
        {
            OnUpdate();
        }

        private void OnUpdate()
        {
            Updated?.Invoke(this, EventArgs.Empty);
        }
    }
}