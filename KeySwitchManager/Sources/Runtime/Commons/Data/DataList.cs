using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KeySwitchManager.Commons.Data
{
    public class DataList<T> : IDataList<T>, IEquatable<DataList<T>>
    {
        private IReadOnlyList<T> List { get; }

        public DataList()
        {
            List = new List<T>();
        }

        public DataList( IEnumerable<T> source )
        {
            List = new List<T>( source );
        }

        #region Implements of C# Collection API
        public int Count => List.Count;

        public IEnumerator<T> GetEnumerator()
            => List.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
        #endregion

        #region Implements of IDataList<T>
        public T this[ int index ] => List[ index ];

        public IReadOnlyCollection<T> ToList()
            => new List<T>( List );
        #endregion

        public bool Equals( DataList<T>? other )
            => other != null && other.SequenceEqual( List );

        public bool SequenceEqual( IEnumerable<T> other, IEqualityComparer<T> comparer )
            => List.SequenceEqual( other, comparer );

        public override int GetHashCode()
        {
            var hashCode = new HashCode();

            foreach( var item in List )
            {
                hashCode.Add( item );
            }

            return hashCode.ToHashCode();
        }
    }
}
