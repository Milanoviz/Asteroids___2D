using System;
using CodeBase.Services.Lives;
using UnityEngine;

namespace CodeBase.HeroModule
{
    public class HeroController : IHeroController
    {
        public Vector2 Position => _hero.Position;
        public float Angle => _hero.Angle;
        public float ActualSpeed => _hero.ActualSpeed;
        public int LaserShotCount => _hero.LaserShotCount;
        public float TimeBeforeShotLaser => -_hero.TimeBeforeShotLaser;

        private readonly IHero _hero;
        private readonly ILivesService _livesService;

        public HeroController(IHero hero, ILivesService livesService)
        {
            _hero = hero;
            _livesService = livesService;
            
            _hero.Hit += HandlerHit;
        }

        private void HandlerHit(object sender, EventArgs e)
        {
            _hero.Die();
            
            GetDamage();
        }

        private void GetDamage()
        {
            _livesService.DecreaseLives();
            
            if (_livesService.CurrentLivesCount > 0)
            {
                _hero.Respwan();
            }
        }
    }
}