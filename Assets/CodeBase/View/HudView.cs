using System;
using CodeBase.HeroModule;
using CodeBase.Infrastructure.States;
using CodeBase.Services.Lives;
using CodeBase.Services.Score;
using TMPro;
using UnityEngine;

namespace CodeBase.View
{
    public class HudView : MonoBehaviour
    {
        [Header("Game Statistic")]
        [SerializeField] private TextMeshProUGUI _liveText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [Header("Hero Statistics")]
        [SerializeField] private TextMeshProUGUI _positionText;
        [SerializeField] private TextMeshProUGUI _angleText;
        [SerializeField] private TextMeshProUGUI _speedText;
        [SerializeField] private TextMeshProUGUI _laserShotCountText;
        [SerializeField] private TextMeshProUGUI _timeBeforShotLaserText;
        [Header("Panel")] 
        [SerializeField] private GameOverPanelView _gameOverPanel;

        private ILivesService _livesService;
        private IScoreService _scoreService;
        private IHeroController _heroController;

        public void Constructor(ILivesService livesService, IScoreService scoreService,
                                IHeroController heroController, GameStateMachine gameStateMachine)
        {
            _gameOverPanel.Constructor(gameStateMachine);
            
            _gameOverPanel.Hide();
            
            _livesService = livesService;
            _scoreService = scoreService;
            _heroController = heroController;
            
            UpdateLives();
            UpdateScore();
            
            _livesService.Die += HandlerDie;
            _livesService.Changed += HandlerLiveChanged;
            _scoreService.Changed += HandlerScoreChanged;
        }

        private void Update()
        {
            UpdateHeroStatistics();
        }

        private void HandlerDie(object sender, EventArgs e)
        {
            _gameOverPanel.Show();
        }

        private void HandlerLiveChanged(object sender, EventArgs e)
        {
            UpdateLives();
        }

        private void HandlerScoreChanged(object sender, EventArgs e)
        {
            UpdateScore();
        }

        private void UpdateLives()
        {
            _liveText.text = $"Lives: {_livesService.CurrentLivesCount}";
        }

        private void UpdateScore()
        {
            _scoreText.text = $"Score: {_scoreService.Score}";
        }

        private void UpdateHeroStatistics()
        {
            _positionText.text = $"Position: {_heroController.Position}";
            _angleText.text = $"Angle: {Math.Round(_heroController.Angle, 2)}";
            _speedText.text = $"Speed: {Math.Round(_heroController.ActualSpeed, 2)}";
            _laserShotCountText.text = $"Laser Shot Count: {_heroController.LaserShotCount}";

            _timeBeforShotLaserText.text = _heroController.TimeBeforeShotLaser < 0 
                ? $"Laser recharge time: {Math.Round(_heroController.TimeBeforeShotLaser, 2)} seconds" 
                : "Laser ready to fire";
            
        }
    }
}