using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// アイコンを持たないクラス
    /// </summary>
    public sealed class EmptyIconHolder : IIconHolder
    {
        Sprite IIconHolder.Icon => null;

        public static readonly EmptyIconHolder Default = new EmptyIconHolder();

        private EmptyIconHolder()
        {
        }
    }
}
