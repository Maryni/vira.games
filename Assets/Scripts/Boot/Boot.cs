using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Global.Boot
{
    using Global;
    using Managers;
    using Tools;
    
    public class Boot : MonoBehaviour
    {
        [SerializeField] private BootSettings bootSettings;
        [SerializeField] private bool needBoot;

        private void Awake()
        {
            ManagersCreation();
            InitBoot();
        }

        private void ManagersCreation()
        {
            List<BaseManager> baseManagers = new List<BaseManager>();
            GameObject managerGameObject = new GameObject("Managers");
            DontDestroyOnLoad(managerGameObject);
            
            for (int i = 0; i < bootSettings.Managers.Count; i++)
            {
                baseManagers.Add(Instantiate(bootSettings.Managers[i], managerGameObject.transform));
            }
            
            //RegisterManagers(locator);
            Services.InitAppWith(baseManagers);
        }

        private void InitBoot()
        {
            if (needBoot)
            {
                Tools.SceneLoader.LoadScene(bootSettings.NextSceneIndex, bootSettings.BootTime);
            }
        }

        // private void RegisterManagers(ManagerLocator locator)
        // {
        //     foreach (BaseManager manager in bootSettings.Managers)
        //     {
        //         locator.Register(manager);
        //     }
        // }
    }
}
