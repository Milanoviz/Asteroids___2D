using CodeBase.Asteroids;
using CodeBase.BulletModule;
using CodeBase.EnemyModule;
using CodeBase.GameData.AsteroidSpawner.Provider;
using CodeBase.GameData.Enemies.Providers;
using CodeBase.GameData.Hero.Provider;
using CodeBase.Helpers;
using CodeBase.HeroModule;
using CodeBase.Services;
using CodeBase.Services.Border;
using CodeBase.Services.InputService;
using CodeBase.Services.Lives;
using CodeBase.Services.Pool;
using CodeBase.Services.Score;
using CodeBase.Services.TickerService;
using CodeBase.View;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IPoolManager _poolManager;
        private readonly AllServices _services;
        private readonly ITicker _ticker;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader,
            LoadingCurtain loadingCurtain, AllServices services, ITicker ticker)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _services = services;
            _ticker = ticker;

            _poolManager = services.Single<IPoolManager>();
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            var inputService = _services.Single<IInputService>();
            var scoreService = _services.Single<IScoreService>();
            var liveService = _services.Single<ILivesService>();
            var borderService = _services.Single<IBorderService>();
            
            var heroGameObject = _poolManager.CreateGameObject(AssetsPath.HeroPath);
            var hero = heroGameObject.GetComponent<Hero>();
            
            hero.Constructor(inputService, _services.Single<IBulletFactory>(),
                                    _services.Single<IHeroDataModelProvider>());
            
            var heroController = new HeroController(hero, liveService);
            
            var asteroidSpawner = new AsteroidsSpawner(_services.Single<IAsteroidsFactory>(), borderService,
                                          _services.Single<IAsteroidSpawnerDataModelProvider>());

            var enemyFactory = new EnemyFactory(_poolManager, borderService, 
                                    _services.Single<IEnemiesDataModelProvider>(), hero);

            var delayBetweenSpawn = _services.Single<IEnemiesDataModelProvider>().DelayBetweenSpawn;
            var enemiesSpawner = new EnemiesSpawner(enemyFactory, delayBetweenSpawn, _ticker);
            
            var scoreCalculator = new ScoreCalculator(scoreService, asteroidSpawner, enemiesSpawner);
            
            var hudView = _poolManager.CreateGameObject(AssetsPath.HudPath).GetComponent<HudView>();
            hudView.Constructor(liveService, scoreService, heroController, _stateMachine);
            
            borderService.AddTrackedItem(hero.transform);
            
            _stateMachine.Enter<GameLoopState>();
        }

    }
}