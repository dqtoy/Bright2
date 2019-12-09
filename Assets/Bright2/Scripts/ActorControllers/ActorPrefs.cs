using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="Actor"/>に対して汎用的に文字列を保持できるクラス
    /// </summary>
    /// <remarks>
    /// 汎用的なフラグを保持するセーブデータとして扱っています
    /// </remarks>
    public sealed class ActorPrefs
    {
        public readonly Dictionary<string, string> Dictionary = new Dictionary<string, string>();

        public void Set(string key, string value)
        {
            if(this.Dictionary.ContainsKey(key))
            {
                this.Dictionary[key] = value;
            }
            else
            {
                this.Dictionary.Add(key, value);
            }
        }

        public void SetAsFlag(string key)
        {
            if(this.Dictionary.ContainsKey(key))
            {
                return;
            }

            this.Dictionary.Add(key, "");
        }

        public void Remove(string key)
        {
            this.Dictionary.Remove(key);
        }

        public bool ContainsKey(string key)
        {
            return this.Dictionary.ContainsKey(key);
        }

        public string Get(string key)
        {
            Assert.IsTrue(this.ContainsKey(key), $"{key}に紐づくデータがありませんでした");

            return this.Dictionary[key];
        }

        public bool GetBool(string key)
        {
            return this.GetAs<bool>(key, (value) => bool.Parse(value));
        }

        public int GetInt(string key)
        {
            return this.GetAs<int>(key, (value) => int.Parse(value));
        }

        public float GetFloat(string key)
        {
            return this.GetAs<float>(key, (value) => float.Parse(value));
        }

        private T GetAs<T>(string key, Func<string, T> parser) where T : struct
        {
            return parser(this.Get(key));
        }
    }
}
