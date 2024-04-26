using GameScript;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Techno
{
    public class Hotspot : MonoBehaviour, IPointerClickHandler, INavigationSuccessListener
    {
        #region Inspector
        [SerializeField]
        private Collider m_ClickCollider;

        [SerializeField]
        private LocalizationReference m_LabelTextId;

        [SerializeField]
        private HotspotLabel m_Label;

        [SerializeField]
        private Marker m_ApproachMarker;

        [SerializeField]
        private ObservableVariableBool m_IsHotspotActive;

        [SerializeField]
        private Collider m_InteractionBoundary;

        [SerializeField]
        private ObservableVariableNavigationRequest m_LastNavigationRequest;

        [SerializeField]
        private ObservableEvent m_OnLocaleChanged;

        [SerializeReference, SubclassSelector]
        private IAction[] m_ActionList;
        #endregion

        #region State
        private Localization m_LocalizedLabelText;
        private bool m_InBoundary;
        #endregion

        #region Event System Events
        public void OnPointerClick(PointerEventData eventData)
        {
            // Ignore if inactive
            if (!m_IsHotspotActive.Value)
                return;

            NavigationRequest navigationRequest =
                new()
                {
                    Position = m_ApproachMarker.transform.position,
                    Rotation = m_ApproachMarker.transform.rotation,
                    WithRotation = true,
                    SuccessListener = this,
                };
            m_LastNavigationRequest.Value = navigationRequest;
        }
        #endregion

        #region Unity Lifecycle Methods
        private void Awake()
        {
            m_LocalizedLabelText = Database.FindLocalization(m_LabelTextId.Id);
            m_IsHotspotActive.RegisterListener(OnActiveChanged);
            m_OnLocaleChanged.RegisterListener(OnTextChanged);
            m_Label.SetFocus(false);
            m_Label.SetActive(false);
        }

        private void OnDestroy()
        {
            m_IsHotspotActive.UnregisterListener(OnActiveChanged);
            m_OnLocaleChanged.UnregisterListener(OnTextChanged);
        }
        #endregion

        #region Child Events
        public void OnClickableEnter()
        {
            m_Label.SetFocus(true);
        }

        public void OnClickableExit()
        {
            m_Label.SetFocus(false);
        }

        public void OnBoundaryEnter()
        {
            m_InBoundary = true;
            OnActiveChanged();
        }

        public void OnBoundaryExit()
        {
            m_InBoundary = false;
            OnActiveChanged();
        }
        #endregion

        #region Helpers
        private void OnActiveChanged()
        {
            m_Label.SetActive(m_IsHotspotActive.Value && m_InBoundary);
        }

        private void OnTextChanged()
        {
            // TODO fetch locale from wherever it's stored
            m_Label.SetText(m_LocalizedLabelText.GetLocalization(Database.Instance.Locales[0]));
        }

        public void OnNavigationSuccess()
        {
            if (m_ActionList != null)
            {
                for (int i = 0; i < m_ActionList.Length; i++)
                {
                    m_ActionList[i].Execute();
                }
            }
        }
        #endregion
    }
}
