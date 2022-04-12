using System;

namespace CodeBase.Services.TickerService
{
    public interface ITicker : IService
    {
        event EventHandler Updated;
    }
}