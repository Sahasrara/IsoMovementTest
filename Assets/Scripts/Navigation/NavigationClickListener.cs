using UnityEngine;
using UnityEngine.EventSystems;

namespace Techno
{
    public class NavigationClickListener : MonoBehaviour, IPointerClickHandler
    {
        #region Inspector
        [SerializeField]
        private ObservableVariableNavigationRequest m_LastNavigationRequest;
        #endregion

        #region Event System Handlers
        public void OnPointerClick(PointerEventData eventData)
        {
            NavigationRequest navigationRequest =
                new()
                {
                    Position = eventData.pointerCurrentRaycast.worldPosition,
                    WithRotation = false
                };
            m_LastNavigationRequest.Value = navigationRequest;
        }
        #endregion
    }
}
