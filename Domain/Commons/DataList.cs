using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KeySwitchManager.Domain.Commons
{
    public class DataList<T> : IDataList<T>, IEquatable<DataList<T>>
    {
        private IReadOnlyCollection<T> List { get; }

        public DataList( IReadOnlyCollection<T> source )
        {
            List = new List<T>( source );
        }

        #region Implements of Collection API
        public int Count => List.Count;

        public IEnumerator<T> GetEnumerator() => List.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion

        public bool Equals( DataList<T>? other )
        {
            return other != null && other.SequenceEqual( List );
        }
    }
}