using UnityEngine;
using UnityEngine.EventSystems;

namespace Techno
{
    public class NavigationClickListener : MonoBehaviour, IPointerClickHandler
    {
        #region Inspector
        [SerializeField]
        private ObservableVariablePositionAndRotation m_LastNavigationRequest;
        #endregion

        #region Event System Handlers
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("NAV");
            PositionAndRotation positionAndRotation = new();
            positionAndRotation.Position = eventData.pointerCurrentRaycast.worldPosition;
            positionAndRotation.Rotation = Quaternion.identity;
            m_LastNavigationRequest.Value = positionAndRotation;
        }
        #endregion
    }
}
