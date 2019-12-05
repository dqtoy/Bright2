using System;

namespace HK.Bright2.UniTerminal
{
    /// <summary>
    /// <see cref="UniTerminalCore"/>で実行可能な関数属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple=false)]
    public sealed class UniTerminalCommandAttribute : Attribute
    {
        public UniTerminalCommandAttribute()
        {
        }
    }
}
