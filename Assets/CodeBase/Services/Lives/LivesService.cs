using System;

namespace CodeBase.Services.Lives
{
    public class LivesService : ILivesService
    {
        public event EventHandler Changed;
        public event EventHandler Die;

        public int CurrentLivesCount => _currentLivesCount;

        private int _currentLivesCount;

        public LivesService(int currentLivesCount)
        {
            _currentLivesCount = currentLivesCount;
        }

        public void DecreaseLives()
        {
            _currentLivesCount--;
            if (_currentLivesCount <= 0)
            {
                OnDie();
            }
            OnChanged();
        }
        
        private void OnChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }

        private void OnDie()
        {
            Die?.Invoke(this, EventArgs.Empty);
        }
    }
}