using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UniTerminal
{
    /// <summary>
    /// 
    /// </summary>
    public static class TestDebugMethods
    {
        [UniTerminalCommand("tm")]
        public static int TestMethod(int hoge, float fuga, string message)
        {
            Debug.Log("Test Method");
            return 0;
        }
    }
}
