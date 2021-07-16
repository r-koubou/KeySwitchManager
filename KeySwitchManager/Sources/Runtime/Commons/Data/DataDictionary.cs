using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KeySwitchManager.Commons.Data
{
    public class DataDictionary<TKey, TValue>
        : IDataDictionary<TKey, TValue>, IEquatable<DataDictionary<TKey, TValue>>
        where TKey : notnull
        where TValue : notnull
    {
        protected IReadOnlyDictionary<TKey, TValue> Dictionary { get; }

        public DataDictionary()
        {
            Dictionary = new Dictionary<TKey, TValue>();
        }

        public DataDictionary( IReadOnlyDictionary<TKey, TValue> dictionary )
        {
            Dictionary = dictionary;
        }

        public bool SequenceEqual(
            DataDictionary<TKey, TValue> other,
            IEqualityComparer<KeyValuePair<TKey, TValue>> comparer )
            => Dictionary.SequenceEqual( other, comparer );

        #region IEquatable
        public bool Equals( DataDictionary<TKey, TValue>? other )
            => other != null && other.Dictionary.SequenceEqual( Dictionary );
        #endregion

        #region Implements of C# Correction API
        public int Count => Dictionary.Count;

        public IEnumerable<TKey> Keys
            => Dictionary.Keys;

        public IEnumerable<TValue> Values
            => Dictionary.Values;

        public bool ContainsKey( TKey key )
            => Dictionary.ContainsKey( key );

        public bool TryGetValue( TKey key, out TValue value ) =>
            Dictionary.TryGetValue( key, out value! );

        public TValue this[ TKey key ]
            => Dictionary[ key ];

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
            => Dictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        #endregion

        public TValue GetValueOrDefault( TKey key, TValue defaultValue )
        {
            if( Dictionary.TryGetValue( key, out var value ) )
            {
                return value;
            }

            return defaultValue;
        }
    }
}