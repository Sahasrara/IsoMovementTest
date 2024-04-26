using System;
using UnityEngine;

namespace Techno
{
    public abstract class ObservableVariable<T> : Observable
    {
        #region State
        [SerializeField]
        private T m_Value;
        #endregion

        #region Editor
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (Application.isPlaying)
            {
                UnityEditor.EditorApplication.delayCall += Notify;
            }
        }
#endif
        #endregion

        #region API
        public T Value
        {
            get => m_Value;
            set
            {
                m_Value = value;
                Notify();
            }
        }
        #endregion
    }
}
