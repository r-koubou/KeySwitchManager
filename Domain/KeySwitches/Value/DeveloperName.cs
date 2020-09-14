using System;

using ArticulationManager.Common.Utilities;

namespace ArticulationManager.Domain.KeySwitches.Value
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

        public bool Equals( DeveloperName? other )
        {
            return other != null && other.Value == Value;
        }

        public override string ToString() => Value;

    }
}
