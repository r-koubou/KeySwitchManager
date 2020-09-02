using System;

using ArticulationManager.Common.Utilities;

namespace ArticulationManager.Domain.Articulations.Value
{
    /// <summary>
    /// An Articulation name
    /// </summary>
    public class ArticulationName : IEquatable<ArticulationName>
    {
        public string Value { get; }

        public ArticulationName( string name )
        {
            if( StringHelper.IsNullOrTrimEmpty( name ) )
            {
                throw new InvalidNameException( nameof( name ) );
            }
            Value = name;
        }

        public bool Equals( ArticulationName? other )
        {
            return other != null && other.Value == Value;
        }

        public override string ToString() => Value;

    }
}
