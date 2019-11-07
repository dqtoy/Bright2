using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UIControllers
{
    /// <summary>
    /// トークUIを制御するクラス
    /// </summary>
    public sealed class TalkUIController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI message = default;

        private void SetMessage(string message)
        {
            this.message.text = message;
        }
    }
}
