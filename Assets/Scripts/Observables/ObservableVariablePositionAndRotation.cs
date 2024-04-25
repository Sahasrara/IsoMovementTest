using System;
using UnityEngine;

namespace Techno
{
    [CreateAssetMenu(
        fileName = "PositionAndRotationVariable",
        menuName = "Techno/Observables/Create Position and Rotation"
    )]
    public class ObservableVariablePositionAndRotation : ObservableVariable<PositionAndRotation> { }

    [Serializable]
    public struct PositionAndRotation
    {
        [SerializeField]
        public Vector3 Position;

        [SerializeField]
        public Quaternion Rotation;
    }
}
