using UnityEngine;

namespace Global.Controllers.Crowd
{
    public class PointBase : MonoBehaviour
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