using System;
using System.Diagnostics.CodeAnalysis;

using ArticulationManager.Common.Utilities;

namespace ArticulationManager.Domain.Articulations.Value
{
    /// <summary>
    /// A DeveloperName name
    /// </summary>
    public class DeveloperName : IEquatable<DeveloperName>
    {
        public string Value { get; }

        public DeveloperName( string name )
        {
            if( StringHelper.IsNullOrTrimEmpty( name ) )
            {
                throw new InvalidNameException( nameof( name ) );
            }
            Value = name;
        }

        public bool Equals( [AllowNull] DeveloperName other )
        {
            return other != null && other.Value == Value;
        }

        public override string ToString() => Value;

    }
}
