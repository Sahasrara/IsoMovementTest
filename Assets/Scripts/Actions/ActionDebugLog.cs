using UnityEngine;

namespace Techno
{
    public class ActionDebugLog : ActionBase
    {
        #region Inspector
        [SerializeField]
        private string m_TextToLog;
        #endregion

        #region API
        public override void Execute() => Debug.Log(m_TextToLog);
        #endregion
    }
}
