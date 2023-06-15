using System.Collections.Generic;
using UnityEngine;

namespace Global.Controllers.Crowd
{
    public class CrowdCalculator : MonoBehaviour
    {
        private readonly List<Vector3> spawnPositions = new();
        
        public int CrowdSize = 10; 
        public float Spacing = 1.0f;
        
        [SerializeField] private Vector3 basePosition;
        
        public List<Vector3> CalculateSpawnPositions()
        {

            Vector3 playerPosition;
            if (transform.childCount == 0)
            {
                playerPosition = Vector3.zero;
            }
            else
            {
                playerPosition = transform.GetChild(0).position;
            }

            for (var i = 0; i < CrowdSize; i++)
            {
                var position = new Vector3(playerPosition.x + Spacing * i, playerPosition.y, playerPosition.z);
                spawnPositions.Add(position);
            }
            
            return spawnPositions;
        }
    }
}