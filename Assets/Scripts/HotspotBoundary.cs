using UnityEngine;
using UnityEngine.Events;

namespace Techno
{
    public class HotspotBoundary : MonoBehaviour
    {
        #region Inspector
        [SerializeField]
        private UnityEvent m_OnPointerExitListeners;

        [SerializeField]
        private UnityEvent m_OnPointerEnterListeners;
        #endregion

        #region Physics Events
        private void OnTriggerExit(Collider other)
        {
            m_OnPointerExitListeners?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            m_OnPointerEnterListeners?.Invoke();
        }
        #endregion
    }
}
