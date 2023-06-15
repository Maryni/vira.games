using System;
using Global.Controllers.Crowd;
using UnityEngine;

namespace Global.Controllers.Gate
{
    public enum CalculationType
    {
        Plus,
        Minus,
        Multiple,
        Division
    }
    public class GateController : MonoBehaviour
    {
        [SerializeField] private CalculationType calculationType;
        [SerializeField] private int value;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<CrowdController>())
            {
                other.GetComponent<CrowdController>().ChangeCount(calculationType, value);
            }
        }
    }
}