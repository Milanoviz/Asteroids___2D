using System;
using CodeBase.BulletModule;
using UnityEngine;

namespace CodeBase.GameData.Bullets.Model
{
    [Serializable]
    public class BulletDataModel
    {
        [SerializeField]
        private BulletType _type;
        [SerializeField]
        private string _path;
        
        public BulletType Type => _type;
        public string Path => _path;
    }
}