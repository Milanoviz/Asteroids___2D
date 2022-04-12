using CodeBase.Infrastructure;
using CodeBase.Infrastructure.States;
using CodeBase.Services;
using CodeBase.Services.TickerService;
using CodeBase.View;

namespace CodeBase
{
    public class Game
    {
        public GameStateMachine StateMachine { get; }
       
        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain, ITicker ticker)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container, ticker);
        }
    }
}