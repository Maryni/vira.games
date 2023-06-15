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
        [SerializeField] private List<Vector3> positionList;

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
            Services.GetManager<EntityManager>().SpawnPlayer(playerMovementZone.transform);
            SetAllPosToAllPlayers();
        }

        private void MoveMoveZone()
        {
            playerMovementZoneRigidbody.AddForce(Vector3.forward * modSpeed * Time.fixedDeltaTime, ForceMode.Force);
        }

        private void SetVariables()
        {
            positionList = new List<Vector3>();
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
            Debug.Log($"list.count = {list.Count}");
            positionList.Clear();

            foreach (var pos in list)
            {
                positionList.Add(pos);
            }
        }

        private void SetNewVariables()
        {
            crowdCalculator.CrowdSize = countPlayers;
        }

        private void SetAllPosToAllPlayers()
        {
            CalculatePosition();
            
            var list = Services.GetManager<EntityManager>().GetAll();
            for (int i = 0; i < list.Count; i++)
            {
                list[i].transform.position = positionList[i];
            }
        }

        // private bool IsHavePosition(Vector3 position)
        // {
        //     PointBase tempBase = new PointBase(position, true);
        //     
        //     for (int i = 0; i < positionList.Count; i++)
        //     {
        //         if (positionList[i].Position == tempBase.Position && positionList[i].Captured == tempBase.Captured)
        //         {
        //             return true;
        //         }
        //     }
        //     tempBase = new PointBase(position, false);
        //     for (int i = 0; i < positionList.Count; i++)
        //     {
        //         if (positionList[i].Position == tempBase.Position && positionList[i].Captured == tempBase.Captured)
        //         {
        //             return true;
        //         }
        //     }
        //
        //     return false;
        // }
    }
}
