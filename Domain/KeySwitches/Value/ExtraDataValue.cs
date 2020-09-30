using System;

using KeySwitchManager.Common.Text;
using KeySwitchManager.Domain.Commons;

namespace KeySwitchManager.Domain.KeySwitches.Value
{
    public class ExtraDataValue : IDictionaryKey<string>, IEquatable<ExtraDataValue>
    {
        public static readonly ExtraDataValue Empty = new ExtraDataValue();

        public string Value { get; }

        private ExtraDataValue()
        {
            Value = "";
        }

        public ExtraDataValue( string value )
        {
            StringHelper.ValidateNullOrTrimEmpty( value );
            Value = value;
        }

        public bool Equals( ExtraDataValue? other )
        {
            return other != null && other.Value == Value;
        }

        public override string ToString() => Value;
    }
}