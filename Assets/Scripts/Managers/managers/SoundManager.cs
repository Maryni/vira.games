using System;
using UnityEngine;

namespace Managers
{
    public class SoundManager : BaseManager
    {
        public override Type ManagerType => typeof(SoundManager);

        protected override bool OnInit()
        {
            Debug.Log($"SoundManager initialized");
            return true;
        }
    }
}
