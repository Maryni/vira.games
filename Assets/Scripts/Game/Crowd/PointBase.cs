using System;
using UnityEngine;

namespace Global.Controllers.Crowd
{
    [Serializable]
    public struct PointBase 
    {
        public Vector3 Position;
        public bool Captured;

        public PointBase(Vector3 value, bool isCaptured)
        {
            Position = value;
            Captured = isCaptured;
        }
    }
}