using System;

using ValueObjectGenerator;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Models.Values
{
    [ValueObject(typeof(Guid), Option = ValueOption.NonValidating)]
    public partial class GuidCell
    {
        public static readonly GuidCell Empty = new(Guid.Empty);
    }
}