using System;
using UnityEngine;

namespace Techno
{
    [Serializable]
    public struct NavigationRequest
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public bool WithRotation;
        public INavigationSuccessListener SuccessListener;
    }
}
