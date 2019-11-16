using System;
using System.Collections.Generic;
using HK.Bright2.GameSystems;
using HK.Framework.EventSystems;

namespace HK.Bright2.UIControllers.Messages
{
    /// <summary>
    /// グリッドUIの表示をリクエストするメッセージ
    /// </summary>
    public sealed class RequestShowGridUI : Message<RequestShowGridUI, IEnumerable<IIconHolder>, Action<int>, Action>
    {
        public IEnumerable<IIconHolder> Items => this.param1;

        public Action<int> OnDecidedIndex => this.param2;

        public Action OnCancelFromUserInput => this.param3;
    }
}
