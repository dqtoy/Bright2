using UnityEngine;
using UnityEngine.Assertions;
using HK.Bright2.GameSystems;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HK.Bright2.StageControllers
{
    /// <summary>
    /// ステージの中核となるクラス
    /// </summary>
    public sealed class Stage : MonoBehaviour
    {
        [SerializeField]
        private Transform startPoint = default;
        public Transform StartPoint => this.startPoint;

#if UNITY_EDITOR
        [ContextMenu("Set in PlayMode")]
        private void SetInPlayMode()
        {
            var createStageOnStart = Object.FindObjectOfType<CreateStageOnStart>();
            if(createStageOnStart == null)
            {
                var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
                Assert.IsTrue(false, $"{scene.name}に{typeof(CreateStageOnStart)}がありませんでした");
                return;
            }

            createStageOnStart.SetPrefab(this);
        }
#endif
    }
}
