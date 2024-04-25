using GameScript;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Techno
{
    public class Hotspot : MonoBehaviour, IPointerClickHandler
    {
        #region Inspector
        [SerializeField]
        private Collider m_ClickCollider;

        [SerializeField]
        private LocalizationReference m_LabelTextId;

        [SerializeField]
        private Marker m_CenterMarker;

        [SerializeField]
        private Marker m_ApproachMarker;

        [SerializeField]
        private ObservableVariableBool m_IsHotspotActive;

        [SerializeField]
        private Collider m_InteractionBoundary;

        [SerializeField]
        private ObservableVariableVector3 m_LastNavigationRequest;

        [SerializeField]
        private ActionBase[] m_ActionList;
        #endregion

        #region Event System Events
        public void OnPointerClick(PointerEventData eventData)
        {
            m_LastNavigationRequest.Value = m_ApproachMarker.transform.position;

            // TODO
            // Add in direction to this observable
            // add in observable for when the character has arrived OR create an event bus
        }
        #endregion
    }
}
