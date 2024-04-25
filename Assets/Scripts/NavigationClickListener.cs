using UnityEngine;
using UnityEngine.EventSystems;

namespace Techno
{
    public class NavigationClickListener : MonoBehaviour, IPointerClickHandler
    {
        #region Inspector
        [SerializeField]
        private ObservableVariableVector3 m_LastNavigationRequest;
        #endregion

        #region Event System Handlers
        public void OnPointerClick(PointerEventData eventData)
        {
            m_LastNavigationRequest.Value = eventData.pointerCurrentRaycast.worldPosition;
        }
        #endregion
    }
}
