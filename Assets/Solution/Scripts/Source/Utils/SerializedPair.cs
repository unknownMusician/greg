using System;
using System.Collections.Generic;

namespace Greg.Utils
{
    [Serializable]
    public struct SerializedPair<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;

        public SerializedPair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public readonly KeyValuePair<TKey, TValue> ToKeyValuePair()
        {
            return new KeyValuePair<TKey, TValue>(Key, Value);
        }
    }

    public static class SerializedPair
    {
        public static SerializedPair<TKey, TValue> From<TKey, TValue>(TKey key, TValue value)
        {
            return new SerializedPair<TKey, TValue>(key, value);
        }
        
        public static SerializedPair<TKey, TValue> From<TKey, TValue>(KeyValuePair<TKey, TValue> pair)
        {
            return new SerializedPair<TKey, TValue>(pair.Key, pair.Value);
        }
    }
}