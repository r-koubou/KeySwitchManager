using System;

using ValueObjectGenerator;

namespace KeySwitchManager.Domain.Commons
{
    [ValueObject(typeof(Guid), Option = ValueOption.NonValidating | ValueOption.ToString)]
    public partial class EntityGuid
    {
        private partial string ToStringImpl() => Value.ToString( "D" );
    }
}