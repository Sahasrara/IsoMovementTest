using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Techno
{
    public class HotspotLabel : MonoBehaviour
    {
        #region Inspector
        [SerializeField]
        private TextMeshProUGUI m_Text;

        [SerializeField]
        private Image m_Icon;

        [SerializeField]
        private Sprite m_IconSpriteBlurred;

        [SerializeField]
        private Sprite m_IconSpriteFocused;

        [SerializeField]
        private CanvasGroup m_CanvasGroup;
        #endregion

        #region Unity Lifecycle Methods
        private void Update() => transform.eulerAngles = new Vector3(30, 15, 0);
        #endregion

        #region API
        public void SetText(string text) => m_Text.text = text;

        public string GetText() => m_Text.text;

        public void SetFocus(bool isFocused) =>
            m_Icon.sprite = isFocused ? m_IconSpriteFocused : m_IconSpriteBlurred;

        public void SetActive(bool isActive) => m_CanvasGroup.alpha = isActive ? 1f : 0f;
        #endregion
    }
}
