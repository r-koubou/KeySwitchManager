using System;

using ArticulationManager.Common.Utilities;

namespace ArticulationManager.Domain.KeySwitches.Value
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
            if( StringHelper.IsNullOrTrimEmpty( name ) )
            {
                name = string.Empty;
            }
            Value = name;
        }

        public bool Equals( Author? other )
        {
            return other != null && other.Value == Value;
        }

        public override string ToString() => Value;

    }
}