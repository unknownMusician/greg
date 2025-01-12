using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Greg.Utils
{
    [Serializable]
    public struct InspectorDictionary<TKey, TValue> : ISerializationCallbackReceiver
    {
        [SerializeField] private SerializedPair<TKey, TValue>[] _serialized;

        public IReadOnlyDictionary<TKey, TValue> Data { get; private set; }

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            Data = new Dictionary<TKey, TValue>(_serialized.Select(p => p.ToKeyValuePair()));
        }
    }
}