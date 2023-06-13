using System;
using UnityEngine;

namespace Managers
{
    public class DataManager : BaseManager
    {
        //to realize
        //[SerializeField] private StaticData staticData; 
        //[SerializeField] private DynamicData dynamicData; 
        public override Type ManagerType => typeof(DataManager);
        private int value = 6;
        public int Value => value;

        protected override bool OnInit()
        {
            Debug.Log($"DataManager initialized");
            return true;
        }
    }
}
