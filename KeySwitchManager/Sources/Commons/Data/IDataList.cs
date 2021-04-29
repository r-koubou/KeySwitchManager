using System.Collections.Generic;

namespace KeySwitchManager.Commons.Data
{
    public interface IDataList<out T> : IReadOnlyList<T>
    {
        public IReadOnlyCollection<T> ToList();
    }
}