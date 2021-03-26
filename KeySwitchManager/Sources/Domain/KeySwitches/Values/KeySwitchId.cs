using System;

using ValueObjectGenerator;

namespace KeySwitchManager.Domain.KeySwitches.Values
{
    [ValueObject(typeof(Guid), Option = ValueOption.NonValidating | ValueOption.ToString)]
    public partial class KeySwitchId
    {
        private partial string ToStringImpl() => Value.ToString( "D" );
    }
}