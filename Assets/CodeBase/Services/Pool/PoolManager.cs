using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Services.Pool
{
    public class PoolManager : IPoolManager
    {
        private readonly Dictionary<string, List<GameObject>> _poolObjects = new Dictionary<string, List<GameObject>>();

        private readonly Dictionary<GameObject, string> _gameObjectToPath = new Dictionary<GameObject, string>();

        public GameObject CreateGameObject(string path)
        {
            var gameObject = Resources.Load<GameObject>(path);
            var instance = Object.Instantiate(gameObject);
            _gameObjectToPath[instance] = path;
            return instance;
        }

        public GameObject GetGameObject(string path)
        {
            GameObject instance;
            
            if (_poolObjects.TryGetValue(path, out var listGameObjects))
            {
                if (HasFreeElement(listGameObjects, out instance))
                {
                    return instance;
                } 
                else
                {
                    instance = CreateGameObject(path);
                    listGameObjects.Add(instance);
                }
            }
            else
            {
                _poolObjects[path] = new List<GameObject>();
                instance = CreateGameObject(path);
                _poolObjects[path].Add(instance);
            }
            
            instance.SetActive(true);
            return instance;
        }

        public void PutGameObjectToPool(GameObject gameObject)
        {
            if (_gameObjectToPath.TryGetValue(gameObject, out var path))
            {
                if (_poolObjects.TryGetValue(path, out var listGameObjects))
                {
                    gameObject.SetActive(false);
                }
            }
        }

        private bool HasFreeElement(List<GameObject> listGameObjects, out GameObject element)
        {
            foreach (var gameObject in listGameObjects)
            {
                if (!gameObject.gameObject.activeInHierarchy)
                {
                    element = gameObject;
                    element.SetActive(true);
                    return true;
                }
            }
            element = null;
            return false;
        }
    }
}