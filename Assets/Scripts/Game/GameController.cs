using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Global.Controllers.Crowd;
using Managers;
using UnityEngine;

namespace Global.Controllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameObject playerMovementZone;
        [SerializeField] private float modSpeed;
        [SerializeField] private List<PointBase> positionList = new List<PointBase>();

        private FormationRenderer formationRenderer;
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
            Services.GetManager<EntityManager>().SpawnPlayer(GetAvailablePosition(), playerMovementZone.transform);
        }

        private void MoveMoveZone()
        {
            playerMovementZoneRigidbody.AddForce(Vector3.forward * modSpeed * Time.fixedDeltaTime, ForceMode.Force);
        }

        private void SetVariables()
        {
            playerMovementZoneGameObject = playerMovementZone.transform.GetChild(0).gameObject;
            playerMovementZoneRigidbody = playerMovementZoneGameObject.GetComponent<Rigidbody>();
            formationRenderer = playerMovementZoneRigidbody.GetComponent<FormationRenderer>();
            boxFormation = playerMovementZoneRigidbody.GetComponent<BoxFormation>();
        }

        private void SetPlayersCount()
        { 
            countPlayers = playerMovementZoneRigidbody.transform.childCount;
        }
        
        private void CalculatePosition()
        {
            SetNewVariables();
            
            var list = formationRenderer.GetPoints();
            foreach (var pos in list)
            {
                positionList.Add(new PointBase(pos, false));
            }
            
        }

        private void SetNewVariables()
        {
            if (countPlayers == 0 || countPlayers == 1)
            {
                boxFormation.SetWidthAndDepth(1, 1);
            }
            if (countPlayers % 2 == 0)
            {
                boxFormation.SetWidthAndDepth(countPlayers/2, countPlayers /2);  
            }
            else
            {
                boxFormation.SetWidthAndDepth((countPlayers/2) + 1, countPlayers /2);  
            }
        }

        private Vector3 GetAvailablePosition()
        {
            var position = positionList.FirstOrDefault(x => !x.Captured);
            if (position != null)
            {
                return position.Position;
            }
            else
            {
                throw new Exception("There is no position available");
            }
        }
    }
}
