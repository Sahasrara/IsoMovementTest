using System;
using UnityEngine;
using UnityEngine.Events;

namespace Techno
{
    [Serializable]
    public class ActionCustom : IAction
    {
        #region Inspector
        [SerializeField]
        private UnityEvent m_CustomAction;
        #endregion

        #region API
        public void Execute() => m_CustomAction?.Invoke();
        #endregion
    }
}
