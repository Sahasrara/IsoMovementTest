using System;
using UnityEngine;

namespace Techno
{
    public abstract class Observable : ScriptableObject
    {
        #region Inspector
        [SerializeField]
        private string m_DeveloperNotes;

        [SerializeField]
        private bool m_FireOnRegister;
        #endregion

        #region State
        private event Action m_OnNotify; // If we want "priority" this could a list of objects.
        #endregion

        #region API
        public bool HasListeners()
        {
            return m_OnNotify.GetInvocationList().Length > 0;
        }

        public void RegisterListener(Action listener)
        {
            m_OnNotify += listener;
            if (m_FireOnRegister)
                listener();
        }

        public void UnregisterListener(Action listener)
        {
            m_OnNotify -= listener;
        }

        public void UnregisterAll()
        {
            m_OnNotify = null;
        }

        public void Notify()
        {
            m_OnNotify?.Invoke();
        }
        #endregion
    }
}
