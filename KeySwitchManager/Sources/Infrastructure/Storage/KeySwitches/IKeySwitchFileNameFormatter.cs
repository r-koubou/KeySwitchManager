using System.Collections.Generic;
using System.Linq;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.Infrastructure.Storage.KeySwitches
{
    public interface IKeySwitchFileNameFormatter
    {
        string Suffix { get; }
        string Format( KeySwitch keySwitch );
        string Format( IReadOnlyCollection<KeySwitch> keySwitches );

        public class Default : IKeySwitchFileNameFormatter
        {
            public string Suffix { get; }

            public Default( string suffix )
            {
                Suffix = suffix;
            }

            public string Format( KeySwitch keySwitch )
                => $"{keySwitch.ProductName} {keySwitch.InstrumentName}.{Suffix}";

            public string Format( IReadOnlyCollection<KeySwitch> keySwitches )
                => $"{keySwitches.First().ProductName}.{Suffix}";
        }
    }
}
