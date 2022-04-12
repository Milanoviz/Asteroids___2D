using CodeBase.Asteroids;
using CodeBase.EnemyModule;

namespace CodeBase.Services.Score
{
    public class ScoreCalculator : IScoreCalculator
    {
        private readonly IScoreService _scoreService;
        private readonly IAsteroidsSpawner _asteroidsSpawner;
        private readonly IEnemiesSpawner _enemiesSpawner;

        public ScoreCalculator(IScoreService scoreService, IAsteroidsSpawner asteroidsSpawner, IEnemiesSpawner enemiesSpawner)
        {
            _scoreService = scoreService;
            _asteroidsSpawner = asteroidsSpawner;
            _enemiesSpawner = enemiesSpawner;
            
            _asteroidsSpawner.DestroyedSuccessfully += HandlerDestroyedSuccessfully;
            _enemiesSpawner.DestroyedSuccessfully += HandlerDestroyedSuccessfully;
        }

        private void HandlerDestroyedSuccessfully(object sender, int score)
        {
            _scoreService.Add(score);
        }
    }
}