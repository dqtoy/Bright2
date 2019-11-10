using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.Database
{
    /// <summary>
    /// <see cref="MasterData"/>が管理する要素
    /// </summary>
    public abstract class MasterDataList<E> : ScriptableObject, IMasterDataList<E> where E : IMasterDataRecord
    {
        [SerializeField]
        private List<E> records = default;
        public IReadOnlyList<E> Records => this.records;

#if UNITY_EDITOR
        public List<E> EditableList => this.records;
#endif
    }
}
