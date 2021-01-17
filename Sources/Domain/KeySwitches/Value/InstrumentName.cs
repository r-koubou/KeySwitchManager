using System;

using KeySwitchManager.Domain.Commons;

using RkHelper.Text;

using ValueObjectGenerator;

namespace KeySwitchManager.Domain.KeySwitches.Value
{
    /// <summary>
    /// An Instrument name
    /// </summary>
    [ValueObject( typeof( string ) )]
    public partial class InstrumentName
    {
        private static partial string Validate( string value )
        {
            StringHelper.ValidateEmpty( value );
            return value;
        }
    }
}