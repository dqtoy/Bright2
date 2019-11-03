using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.Messages.Fade
{
    /// <summary>
    /// フェードインをリクエストするメッセージ
    /// </summary>
    public sealed class RequestFadeIn : Message<RequestFadeIn>
    {
    }

    /// <summary>
    /// フェードアウトをリクエストするメッセージ
    /// </summary>
    public sealed class RequestFadeOut : Message<RequestFadeOut>
    {
    }

    /// <summary>
    /// フェードインを開始した際のメッセージ
    /// </summary>
    public sealed class BeginFadeIn : Message<BeginFadeIn>
    {
    }

    /// <summary>
    /// フェードインを終了した際のメッセージ
    /// </summary>
    public sealed class EndFadeIn : Message<EndFadeIn>
    {
    }

    /// <summary>
    /// フェードアウトを開始した際のメッセージ
    /// </summary>
    public sealed class BeginFadeOut : Message<BeginFadeOut>
    {
    }

    /// <summary>
    /// フェードアウトを終了した際のメッセージ
    /// </summary>
    public sealed class EndFadeOut : Message<EndFadeOut>
    {
    }
}
