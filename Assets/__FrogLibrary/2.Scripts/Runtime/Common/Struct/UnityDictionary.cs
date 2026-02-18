using System.Collections;
using System.Runtime.Serialization;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace FrogLibrary
{
    [Serializable]
    public class UnityDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        [SerializeField] private List<TKey> m_keys = new List<TKey>();
        [SerializeField] private List<TValue> m_values = new List<TValue>();

        public UnityDictionary()
        {
            
        }

        public UnityDictionary(int capacity)
        {
            m_keys.Capacity = capacity;
            m_values.Capacity = capacity;
        }

        public bool ContainsKey(TKey key)
        {
            return m_keys.Contains(key);
        }

        public void Add(TKey key, TValue value)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (m_keys.Contains(key))
                throw new ArgumentException("key already exit " + key);
            m_keys.Add(key);
            m_values.Add(value);
        }

        public bool Remove(TKey key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (!m_keys.Contains(key)) return false;
            var index = m_keys.IndexOf(key);
            m_keys.RemoveAt(index);
            m_values.RemoveAt(index);
            return true;
        }

        // Private method for removing the key/value pair at a particular index
        // This should never be public; dictionaries aren't supposed to have any
        // ordering on their elements, so the idea of an element at a particular
        // index isn't valid in the outside world. That we're using indexable
        // lists for storing keys/values is an implementation detail.
        private void RemoveAt(int index)
        {
            if (index >= m_keys.Count) throw new ArgumentOutOfRangeException(nameof(index));
            m_keys.RemoveAt(index);
            m_values.RemoveAt(index);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default(TValue);
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (!m_keys.Contains(key)) return false;
            value = m_values[m_keys.IndexOf(key)];
            return true;
        }

        TValue IDictionary<TKey, TValue>.this[TKey key]
        {
            get => this[key];
            set => this[key] = value;
        }

        public TValue this[TKey key]
        {
            get
            {
                if (key == null)
                    throw new ArgumentNullException(nameof(key));

                if (!m_keys.Contains(key))
                    throw new ArgumentException($"key doesn't exist: {key}");

                return m_values[m_keys.IndexOf(key)];
            }
            set
            {
                if (key == null)
                    throw new ArgumentNullException(nameof(key));

                if (!m_keys.Contains(key))
                    throw new ArgumentException($"key doesn't exist: {key}");

                m_values[m_keys.IndexOf(key)] = value;
            }
        }

        #region ICollection implementation

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            m_keys.Clear();
            m_values.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ContainsKey(item.Key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public int Count
        {
            get { return m_keys.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        #endregion

        #region IEnumerable implementation

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return m_keys.Select((t, i) => new KeyValuePair<TKey, TValue>(t, m_values[i])).GetEnumerator();
        }

        #endregion

        #region IEnumerable implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < m_keys.Count; i++)
            {
                yield return new KeyValuePair<TKey, TValue>(m_keys[i], m_values[i]);
            }
        }

        #endregion

        #region IDictionary implementation

        public ICollection<TKey> Keys
        {
            get { return m_keys.ToArray(); }
        }

        public ICollection<TValue> Values
        {
            get { return m_values.ToArray(); }
        }

        #endregion
    }

    public static class UnityDictionaryExtensions
    {
        public static IReadOnlyDictionary<TK, TV> ToReadOnlyDictionary<TK, TV>(this UnityDictionary<TK, TV> dictionary)
        {
            return dictionary.ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public static Dictionary<TK, TV> ToDictionary<TK, TV>(this UnityDictionary<TK, TV> dictionary)
        {
            return dictionary.ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}