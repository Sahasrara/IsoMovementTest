using UnityEngine;

namespace Techno
{
    public class TriggerMasked : MonoBehaviour
    {
        #region Inspector
        [SerializeField]
        private TriggerMask m_TriggerMask;
        #endregion

        #region
        public bool IsAffectedByTrigger(TriggerMask mask) => (m_TriggerMask & mask) != 0;
        #endregion
    }
}
