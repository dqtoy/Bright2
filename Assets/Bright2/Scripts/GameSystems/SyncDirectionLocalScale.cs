using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2
{
    /// <summary>
    /// <see cref="Constants.Direction"/>から<see cref="Transform.localScale"/>を設定するクラス
    /// </summary>
    public sealed class SyncDirectionLocalScale : MonoBehaviour, ISyncDirection
    {
        [SerializeField]
        private Vector3 leftScale = default;

        [SerializeField]
        private Vector3 rightScale = default;

        void ISyncDirection.Sync(Constants.Direction direction)
        {
            switch(direction)
            {
                case Constants.Direction.Left:
                    this.transform.localScale = this.leftScale;
                    break;
                case Constants.Direction.Right:
                    this.transform.localScale = this.rightScale;
                    break;
                default:
                    Assert.IsTrue(false, $"{direction}は未対応です");
                    break;
            }
        }
    }
}
