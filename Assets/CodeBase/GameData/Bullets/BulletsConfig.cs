using System.Collections.Generic;
using CodeBase.GameData.Bullets.Model;
using UnityEngine;

namespace CodeBase.GameData.Bullets
{
    [CreateAssetMenu(fileName = "BulletsConfig", menuName = "Configs/Bullets", order = 1)]
    public class BulletsConfig : ScriptableObject
    {
        [SerializeField] private List<BulletDataModel> _bulletsDataModels;

        public List<BulletDataModel> BulletsDataModels => _bulletsDataModels;
    }
}