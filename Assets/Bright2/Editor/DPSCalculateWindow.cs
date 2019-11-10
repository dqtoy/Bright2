using UnityEngine;
using UnityEngine.Assertions;
using UnityEditor;

namespace HK.Bright2
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DPSCalculateWindow : EditorWindow
    {
        private int damage;

        private float coolTime;

        private float criticalRate;

        [MenuItem("HK/Bright2/DPS Calculator")]
        private static void CreateWindow()
        {
            var window = EditorWindow.GetWindow<DPSCalculateWindow>();
        }

        void OnGUI()
        {
            this.damage = EditorGUILayout.IntField("damage", this.damage);
            this.coolTime = EditorGUILayout.FloatField("coolTime", this.coolTime);
            this.criticalRate = EditorGUILayout.FloatField("criticalRate", this.criticalRate);

            var dps = (this.damage + (this.damage / 2.0f) * this.criticalRate) / this.coolTime;
            EditorGUILayout.LabelField($"DPS = {dps}");
        }
    }
}
