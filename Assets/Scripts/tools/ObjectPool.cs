using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace tools.Old
{
    public class ObjectPool<T> : MonoBehaviour 
    {
        [SerializeField] private GameObject exampleObject;
        [SerializeField] private int countSpawnedObjects;
        [SerializeField] private List<GameObject> spawnedObjects;
        [SerializeField] private Transform parentTransform;

        public void Init()
        {
            for (int i = 0; i < countSpawnedObjects; i++)
            {
                var newObject = Instantiate(exampleObject, parentTransform);
                newObject.SetActive(false);
                spawnedObjects.Add(newObject);
            }
        }
        
        public T GetObject()
        {
            var result = spawnedObjects.FirstOrDefault(x => !x.activeSelf);
            if (result == null)
            {
                var created = Instantiate(exampleObject, parentTransform);
                return created.GetComponent<T>();
            }

            return result.GetComponent<T>();
        }
    }
}