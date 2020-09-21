using System;

using KeySwitchManager.Domain.KeySwitches.Value;

namespace KeySwitchManager.Xlsx.KeySwitches.Models
{
    public class ArticulationTypeCell
    {
        public static readonly ArticulationTypeCell Direction = new ArticulationTypeCell( ArticulationType.Direction.ToString() );
        public static readonly ArticulationTypeCell Attribute = new ArticulationTypeCell( ArticulationType.Attribute.ToString() );
        public static readonly ArticulationTypeCell Default = Direction;

        public static ArticulationTypeCell Parse( string value )
        {
            if( value == Direction.Value )
            {
                return Direction;
            }

            if( value == Attribute.Value )
            {
                return Attribute;
            }

            throw new ArgumentException( nameof( value ) );
        }

        public string Value { get; }

        private ArticulationTypeCell( string name )
        {
            Value = name;
        }
    }
}