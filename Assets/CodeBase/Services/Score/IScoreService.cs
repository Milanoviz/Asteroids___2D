using System;

namespace CodeBase.Services.Score
{
    public interface IScoreService : IService
    {
        event EventHandler Changed;
        
        int Score { get; }
        
        void Add(int score);
        void Reset();
    }
}