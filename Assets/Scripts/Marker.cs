using UnityEditor;
using UnityEngine;

namespace Techno
{
    public class Marker : MonoBehaviour
    {
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.color = Color.yellow;
            Handles.ArrowHandleCap(
                0,
                transform.position,
                transform.rotation,
                0.5f,
                EventType.Repaint
            );
        }
#endif
    }
}
