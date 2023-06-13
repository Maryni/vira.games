using System;
using UnityEngine;

namespace Managers
{
    public abstract class BaseManager : MonoBehaviour
    {
        public abstract Type ManagerType { get; }

        public bool isInit { get; private set; } = false;

        public void Init()
        {
            if (!isInit)
            {
                isInit = OnInit();
                if (!isInit)
                {
                    throw new Exception($"Service type of {ManagerType} is not initialazed correctly");
                }
            }
        }

        protected abstract bool OnInit();
    }
}
