using RkHelper.Text;

using ValueObjectGenerator;

namespace KeySwitchManager.Domain.KeySwitches.Value
{
    [ValueObject( typeof( string ) )]
    public partial class ExtraDataValue
    {
        public static readonly ExtraDataValue Empty = new ExtraDataValue();

        private ExtraDataValue()
        {
            Value = string.Empty;
        }

        private static partial string Validate( string value )
        {
            StringHelper.ValidateEmpty( value );
            return value;
        }
    }
}