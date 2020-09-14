using ArticulationManager.Common.Utilities;

namespace ArticulationManager.Domain.Articulations.Value
{
    /// <summary>
    /// An Instrument name
    /// </summary>
    public class InstrumentName
    {
        public string Value { get; }

        public InstrumentName( string name )
        {
            if( StringHelper.IsNullOrTrimEmpty( name ) )
            {
                throw new InvalidNameException( nameof( name ) );
            }
            Value = name;
        }

        public bool Equals( InstrumentName? other )
        {
            return other != null && other.Value == Value;
        }

        public override string ToString() => Value;

    }
}