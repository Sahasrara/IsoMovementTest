using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Techno
{
    public class HotspotClickable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        #region Inspector
        [SerializeField]
        private UnityEvent m_OnPointerExitListeners;

        [SerializeField]
        private UnityEvent m_OnPointerEnterListeners;
        #endregion

        #region Event System Events
        public void OnPointerEnter(PointerEventData eventData)
        {
            m_OnPointerEnterListeners?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            m_OnPointerExitListeners?.Invoke();
        }
        #endregion
    }
}
