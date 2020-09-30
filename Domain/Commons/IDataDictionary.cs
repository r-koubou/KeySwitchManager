using System.Collections.Generic;

namespace KeySwitchManager.Domain.Commons
{
    public interface IDataDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
        where TKey : notnull
        where TValue : notnull
    {
        public IReadOnlyDictionary<TKey, TValue> ToDictionary();
    }
}