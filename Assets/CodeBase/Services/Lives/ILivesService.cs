using System;

namespace CodeBase.Services.Lives
{
    public interface ILivesService : IService
    {
        event EventHandler Changed;
        event EventHandler Die;
        
        int CurrentLivesCount { get; }
        
        void DecreaseLives();
    }
}