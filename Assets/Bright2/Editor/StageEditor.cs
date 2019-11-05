using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.Editor
{
    /// <summary>
    /// 
    /// </summary>
    [InitializeOnLoad]
    public class StageEditor
    {
        static StageEditor()
        {
            PrefabUtility.prefabInstanceUpdated -= OnPrefabInstanceUpdated;
            PrefabUtility.prefabInstanceUpdated += OnPrefabInstanceUpdated;
        }

        private static void OnPrefabInstanceUpdated(GameObject instance)
        {
        }
    }
}
