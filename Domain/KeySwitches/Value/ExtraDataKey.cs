using System;

using KeySwitchManager.Common.Text;
using KeySwitchManager.Domain.Commons;

namespace KeySwitchManager.Domain.KeySwitches.Value
{
    public class ExtraDataKey : IDictionaryKey<string>, IEquatable<ExtraDataKey>
    {
        public string Value { get; }

        public ExtraDataKey( string key )
        {
            StringHelper.ValidateNullOrTrimEmpty( key );
            Value = key;
        }

        public bool Equals( ExtraDataKey? other )
        {
            return other != null && other.Value == Value;
        }

        public override string ToString() => Value;
    }
}