using CodeBase.Asteroids;
using CodeBase.BulletModule;
using CodeBase.EnemyModule;
using CodeBase.GameData.Asteroids;
using CodeBase.GameData.Asteroids.Model;
using CodeBase.GameData.Asteroids.Provider;
using CodeBase.GameData.AsteroidSpawner;
using CodeBase.GameData.AsteroidSpawner.Provider;
using CodeBase.GameData.Bullets;
using CodeBase.GameData.Bullets.Provider;
using CodeBase.GameData.Enemies;
using CodeBase.GameData.Enemies.Providers;
using CodeBase.GameData.Hero;
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
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private const string GameScene = "Main";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly ITicker _ticker;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services, ITicker ticker)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            _ticker = ticker;

            RegisterConfigs();
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>(GameScene);
        }

        private void RegisterConfigs()
        {
            var heroConfig = Resources.Load<HeroConfig>(AssetsPath.HeroConfigPath);
            _services.RegisterSingle<IHeroDataModelProvider>(new HeroDataModelProvider(heroConfig));
            
            var asteroidsConfig = Resources.Load<AsteroidsConfig>(AssetsPath.AsteroidsConfigPath);
            _services.RegisterSingle<IAsteroidDataModelProvider>(new AsteroidDataModelProvider(asteroidsConfig));
            
            var asteroidSpawnerConfig = Resources.Load<AsteroidSpawnerConfig>(AssetsPath.AsteroidSpawnerConfigPath);
            _services.RegisterSingle<IAsteroidSpawnerDataModelProvider>(new AsteroidSpawnerDataModelProvider(asteroidSpawnerConfig));

            var bulletsConfig = Resources.Load<BulletsConfig>(AssetsPath.BulletsConfigPath);
            _services.RegisterSingle<IBulletDataModelProvider>(new BulletDataModelProvider(bulletsConfig));

            var enemiesConfig = Resources.Load<EnemiesConfig>(AssetsPath.EnemiesConfigPath);
            _services.RegisterSingle<IEnemiesDataModelProvider>(new EnemiesDataModelProvider(enemiesConfig));
            
        }
        
        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(GetInputService());
            _services.RegisterSingle<IPoolManager>(new PoolManager());
            _services.RegisterSingle<IBorderService>(new BorderService(_ticker));
            _services.RegisterSingle<ITeleportService>(new TeleportService(_services.Single<IBorderService>()));
            _services.RegisterSingle<IScoreService>(new ScoreService());

            var startLivesCount = _services.Single<IHeroDataModelProvider>().HeroDataModels.LivesCount;
            _services.RegisterSingle<ILivesService>(new LivesService(startLivesCount));
            
            _services.RegisterSingle<IAsteroidsFactory>(
                new AsteroidsFactory(_services.Single<IPoolManager>(),
                                                 _services.Single<IAsteroidDataModelProvider>()));
            
            _services.RegisterSingle<IBulletFactory>(
                new BulletFactory(_services.Single<IPoolManager>(),
                                              _services.Single<IBorderService>(), 
                                          _services.Single<IBulletDataModelProvider>()));
        }
        
        
        public void Exit()
        {
            
        }

        private IInputService GetInputService()
        {
            //TODO: logic to select another desired input type
            
            return new InputKeyboard();
        }
    }
}