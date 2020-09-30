using System;

using KeySwitchManager.Common.Text;

namespace KeySwitchManager.Domain.KeySwitches.Value
{
    /// <summary>
    /// A created author name
    /// </summary>
    public class Author : IEquatable<Author>
    {
        public static readonly Author Empty = new Author( string.Empty );

        public string Value { get; }

        public Author( string name )
        {
            Value = StringHelper.IsNullOrTrimEmpty( name ) ? string.Empty : name;
        }

        public bool Equals( Author? other )
        {
            return other != null && other.Value == Value;
        }

        public override string ToString() => Value;

    }
}