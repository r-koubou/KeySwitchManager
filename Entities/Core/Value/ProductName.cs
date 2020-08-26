using System;
using System.Diagnostics.CodeAnalysis;

using ArticulationManager.Utilities;

namespace ArticulationManager.Entities.Core.Value
{
    /// <summary>
    /// A ProductName name
    /// </summary>
    public class ProductName : IEquatable<ProductName>
    {
        public string Value { get; }

        public ProductName( string name )
        {
            if( StringHelper.IsNullOrTrimEmpty( name ) )
            {
                throw new InvalidNameException( nameof( name ) );
            }
            Value = name;
        }

        public bool Equals( [AllowNull] ProductName other )
        {
            return other != null && other.Value == Value;
        }

        public override string ToString() => Value;

    }
}
