using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UniTerminal
{
    /// <summary>
    /// UniTerminalのコア部分
    /// </summary>
    public sealed class UniTerminalCore
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Setup()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();
            foreach (var t in types)
            {
                foreach(var m in t.GetMethods())
                {
                    var attributes = Attribute.GetCustomAttributes(m, typeof(UniTerminalCommandAttribute));
                    if(attributes.Length > 0)
                    {
                        Debug.Log(m.Name);
                        foreach(var parameter in m.GetParameters())
                        {
                            Debug.Log(parameter.Name);
                        }
                    }
                }
            }
        }
    }
}
