using System;
using UnityEngine;

namespace Techno
{
    [Serializable]
    public class ActionDebugLog : IAction
    {
        #region Inspector
        [SerializeField]
        private string m_TextToLog;
        #endregion

        #region API
        public void Execute() => Debug.Log(m_TextToLog);
        #endregion
    }
}
