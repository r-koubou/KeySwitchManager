using System.Collections.Generic;

namespace KeySwitchManager.Domain.Commons
{
    public interface IDataList<out T> : IReadOnlyList<T>
    {
        public IReadOnlyCollection<T> ToList();
    }
}