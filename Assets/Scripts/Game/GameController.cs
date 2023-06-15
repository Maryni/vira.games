using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Global.Controllers.Crowd;
using Managers;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Global.Controllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameObject playerMovementZone;
        [SerializeField] private float modSpeed;
        [SerializeField] private List<PointBase> positionList;

        private FormationRenderer formationRenderer;
        private CrowdCalculator crowdCalculator;
        private BoxFormation boxFormation;
        private GameObject playerMovementZoneGameObject;
        private Rigidbody playerMovementZoneRigidbody;
        private int countPlayers;

        private void Start()
        { 
            SetVariables();
            SetPlayersCount();
            CalculatePosition();
            SpawnPlayer();
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.X))
            {
                MoveMoveZone();
            }
            if (Input.GetKeyUp(KeyCode.Z))
            {
                SpawnPlayer();
            }
            
        }

        private void SpawnPlayer()
        {
            Vector3 pos = GetAvailablePosition();
            Debug.Log($"SpawnPlayer pos = {pos}");
            Services.GetManager<EntityManager>().SpawnPlayer(pos, playerMovementZone.transform);
        }

        private void MoveMoveZone()
        {
            playerMovementZoneRigidbody.AddForce(Vector3.forward * modSpeed * Time.fixedDeltaTime, ForceMode.Force);
        }

        private void SetVariables()
        {
            positionList = new List<PointBase>();
            playerMovementZoneGameObject = playerMovementZone.transform.GetChild(0).gameObject;
            playerMovementZoneRigidbody = playerMovementZoneGameObject.GetComponent<Rigidbody>();
            crowdCalculator = playerMovementZoneRigidbody.GetComponent<CrowdCalculator>();
            //formationRenderer = playerMovementZoneRigidbody.GetComponent<FormationRenderer>();
            //boxFormation = playerMovementZoneRigidbody.GetComponent<BoxFormation>();
        }

        private void SetPlayersCount()
        { 
            countPlayers = playerMovementZoneRigidbody.transform.childCount;
        }
        
        private void CalculatePosition()
        {
            SetPlayersCount();
            SetNewVariables();

            List<Vector3> list = crowdCalculator.CalculateSpawnPositions();

            foreach (var pos in list)
            {
                var temp = new PointBase(pos, false);
                if (!IsHavePosition(pos, positionList))
                {
                    positionList.Add(temp);
                }
                
                Debug.Log($"reg pos = {pos}");
            }
        }

        private void SetNewVariables()
        {
            crowdCalculator.CrowdSize = countPlayers + 1;
        }

        private Vector3 GetAvailablePosition()
        {
            CalculatePosition();
            PointBase currentPos = positionList.FirstOrDefault(x => x.Captured == false);
            currentPos.Captured = true;
            return currentPos.Position;
        }

        private bool IsHavePosition(Vector3 position, List<PointBase> list)
        {
            PointBase tempBase = new PointBase(position, true);
            
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Position == tempBase.Position && list[i].Captured == tempBase.Captured)
                {
                    return true;
                }
            }
            tempBase = new PointBase(position, false);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Position == tempBase.Position && list[i].Captured == tempBase.Captured)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
