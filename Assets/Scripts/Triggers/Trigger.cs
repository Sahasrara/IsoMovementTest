using UnityEngine;

namespace Techno
{
    public class Trigger : MonoBehaviour
    {
        #region Inspector
        [SerializeField]
        private TriggerMask m_TriggerMask;

        [SerializeField]
        private TriggerType m_TriggerType;

        [SerializeField]
        private ObservableVariableBool m_IsTriggerActive;

        [SerializeReference, SubclassSelector]
        private IAction[] m_ActionList;
        #endregion

        #region Physics Events
        private void OnTriggerExit(Collider other)
        {
            if (((m_TriggerType & TriggerType.Exit) != TriggerType.Exit) || !IsTriggerActive())
                return;
            TriggerMasked masked = other.gameObject.GetComponent<TriggerMasked>();
            if (masked && masked.IsAffectedByTrigger(m_TriggerMask))
                OnTriggered();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (((m_TriggerType & TriggerType.Enter) != TriggerType.Enter) || !IsTriggerActive())
                return;
            TriggerMasked masked = other.gameObject.GetComponent<TriggerMasked>();
            if (masked && masked.IsAffectedByTrigger(m_TriggerMask))
                OnTriggered();
        }
        #endregion

        #region Helpers
        private void OnTriggered()
        {
            if (m_ActionList != null)
            {
                for (int i = 0; i < m_ActionList.Length; i++)
                {
                    m_ActionList[i].Execute();
                }
            }
        }

        private bool IsTriggerActive() => m_IsTriggerActive != null && m_IsTriggerActive.Value;
        #endregion
    }
}
