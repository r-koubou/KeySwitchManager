using System;

namespace ArticulationManager.Domain.Commons
{
    public interface IText : IEquatable<IText>
    {
        public string Value { get; }

        bool IEquatable<IText>.Equals( IText? other )
        {
            return other != null && other.Value == Value;
        }
    }
}