using System;

namespace KeySwitchManager.Domain.Commons
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