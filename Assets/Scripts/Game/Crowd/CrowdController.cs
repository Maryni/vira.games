using System.Collections;
using System.Collections.Generic;
using Global.Controllers.Gate;
using UnityEngine;

public class CrowdController : MonoBehaviour
{
    [SerializeField] private int crowdCount;

    public void ChangeCount(CalculationType type, int value)
    {
        switch (type)
        {
            case CalculationType.Plus:
                crowdCount += value; break;
            case CalculationType.Minus:
                crowdCount -= value; break;
            case CalculationType.Multiple:
                crowdCount = crowdCount * value; break;
            case CalculationType.Division:
                crowdCount = crowdCount / value; break;
        }
    }
}
