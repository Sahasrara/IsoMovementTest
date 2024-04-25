using UnityEngine;
using UnityEngine.Events;

namespace Techno
{
    public class ActionCustom : ActionBase
    {
        #region Inspector
        [SerializeField]
        private UnityAction m_CustomAction;
        #endregion

        #region API
        public override void Execute() => m_CustomAction?.Invoke();
        #endregion
    }
}
