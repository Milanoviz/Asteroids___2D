using CodeBase.Infrastructure;
using CodeBase.Infrastructure.States;
using CodeBase.Services.TickerService;
using CodeBase.View;
using UnityEngine;

namespace CodeBase
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _curtainPrefab;
        [SerializeField] private Ticker _tickerPrefab;

        private Game _game;

        private void Awake()
        {
            ITicker ticker = _tickerPrefab;
            
            _game = new Game(this, Instantiate(_curtainPrefab), ticker);
            _game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}
