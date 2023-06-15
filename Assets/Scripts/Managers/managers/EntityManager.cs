using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Managers
{
    public class EntityManager : BaseManager
    {
        [SerializeField] private GameObject exampleObject;
        [SerializeField] private Transform parentTransform;
        
        private ObjectPool<PlayerComponents> pool;
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

        }
        
        public void SpawnPlayer(Vector3 position, Transform transformValue = null)
        {
            var spawnedObject = pool.Get();

            Debug.Log($"posS = {position}");
            spawnedObject.transform.position = position;
            spawnedObject.transform.SetParent(transformValue.GetChild(0));
            spawnedObject.gameObject.SetActive(true);
        }
    }
}