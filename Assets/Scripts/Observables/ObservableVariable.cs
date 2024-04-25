using UnityEngine;

namespace Techno
{
    public abstract class ObservableVariable<T> : Observable
    {
        #region State
        [SerializeField]
        private T m_Value;
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
