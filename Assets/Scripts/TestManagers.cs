using Managers;
using Global;
using UnityEngine;

public class TestManagers : MonoBehaviour
{
    void Start()
    {
        Debug.Log($"Hi. Value from Services.GetManager<DataManager>().Value = {Services.GetManager<DataManager>().Value}");
    }
    
}
