using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace Managers
{
    public class EntityManager : BaseManager
    {
        [SerializeField] private GameObject exampleObject;
        [SerializeField] private Transform parentTransform;
        
        private ObjectPool<PlayerComponents> pool;
        private List<PlayerComponents> spawnedObjects = new List<PlayerComponents>();
        private int value = 5;

        public override Type ManagerType => typeof(EntityManager);
        protected override bool OnInit()
        {
            pool = new ObjectPool<PlayerComponents>(createFunc:CreatePlayer, maxSize:300);

            Debug.Log($"EntityManager initialized");
            return true;
        }

        private PlayerComponents CreatePlayer()
        {
            var component = Instantiate(exampleObject, parentTransform).GetComponent<PlayerComponents>();
            component.gameObject.SetActive(false);
            return component;
        }

        public void SpawnPlayer(Transform transformValue = null)
        {
            var spawnedObject = pool.Get();
            if (transformValue != null)
            {
                //spawnedObject.transform.position = transformValue.position;
                spawnedObject.transform.SetParent(transformValue.GetChild(0));
            }
            spawnedObject.gameObject.SetActive(true);
            spawnedObjects.Add(spawnedObject);
        }
        
        public void SpawnPlayer(Vector3 position, Transform transformValue = null)
        {
            var spawnedObject = pool.Get();

            Debug.Log($"posS = {position}");
            spawnedObject.transform.SetParent(transformValue.GetChild(0));
            spawnedObject.transform.position = position;
            spawnedObject.gameObject.SetActive(true);
            spawnedObjects.Add(spawnedObject);
        }

        public List<PlayerComponents> GetAll()
        {
            return spawnedObjects;
        }
    }
}