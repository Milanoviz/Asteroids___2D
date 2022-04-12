using System;

namespace CodeBase.Services.Score
{
    public class ScoreService : IScoreService
    {
        public event EventHandler Changed;

        public int Score => _score;

        private int _score;

        public ScoreService()
        {
            _score = 0;
        }

        public void Add(int score)
        {
            _score += score;
            OnChanged();
        }

        public void Reset()
        {
            _score = 0;
            OnChanged();
        }
        
        private void OnChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }
    }
}