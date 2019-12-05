using System;
using System.Collections.Generic;
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
        private static readonly Dictionary<string, MethodInfo> commands = new Dictionary<string, MethodInfo>();
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Setup()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();
            foreach (var t in types)
            {
                foreach(var m in t.GetMethods())
                {
                    var attribute = Attribute.GetCustomAttribute(m, typeof(UniTerminalCommandAttribute)) as UniTerminalCommandAttribute;
                    if(attribute != null)
                    {
                        commands.Add(m.Name, m);
                        foreach(var customCommandName in attribute.CustomCommandNames)
                        {
                            commands.Add(customCommandName, m);
                        }
                    }
                }
            }
        }
    }
}
