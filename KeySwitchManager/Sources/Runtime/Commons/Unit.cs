using System;

namespace KeySwitchManager.Commons
{

    [Serializable]
    public sealed class Unit : IEquatable<Unit>
    {
        public static readonly Unit Default = new();

        private Unit() {}

        public bool Equals( Unit _ )
            => true;

        public override bool Equals( object? obj )
            => ReferenceEquals( this, obj ) || obj is Unit other && Equals( other );

        public override int GetHashCode()
            => 1;

        public static bool operator ==( Unit? left, Unit? right )
            => true;

        public static bool operator !=( Unit? left, Unit? right )
            => !( left == right );
    }
}
