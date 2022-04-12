using CodeBase.Infrastructure;
using CodeBase.Infrastructure.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.View
{
    public class GameOverPanelView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Button _restartButton;

        private GameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;

        public void Constructor(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
        }

        private void OnRestartButtonClicked()
        {
            //TODO : Restart level logic
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}