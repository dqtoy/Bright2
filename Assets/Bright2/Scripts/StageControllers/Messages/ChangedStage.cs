﻿using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.StageControllers.Messages
{
    /// <summary>
    /// <see cref="Stage"/>の切り替えた際のメッセージ
    /// </summary>
    public sealed class ChangedStage : Message<ChangedStage, Stage>
    {
        public Stage NewStage => this.param1;
    }
}
