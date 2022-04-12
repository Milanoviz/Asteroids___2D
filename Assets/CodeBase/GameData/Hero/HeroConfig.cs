using CodeBase.GameData.Hero.Model;
using UnityEngine;

namespace CodeBase.GameData.Hero
{
    [CreateAssetMenu(fileName = "HeroConfig", menuName = "Configs/Hero", order = 1)]
    public class HeroConfig : ScriptableObject
    {
        [SerializeField] private HeroDataModel _heroDataModel;

        public HeroDataModel HeroDataModel => _heroDataModel;
    }
}