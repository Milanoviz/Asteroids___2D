using UnityEngine;

namespace CodeBase.Services.Pool
{
    public interface IPoolManager : IService
    {
        GameObject CreateGameObject(string path);
        
        GameObject GetGameObject(string path);
        void PutGameObjectToPool(GameObject gameObject);
    }
}