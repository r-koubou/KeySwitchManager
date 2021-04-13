using System.Collections.Generic;

namespace KeySwitchManager.Commons.Data
{
    public interface IDataDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
        where TKey : notnull
        where TValue : notnull
    {}
}