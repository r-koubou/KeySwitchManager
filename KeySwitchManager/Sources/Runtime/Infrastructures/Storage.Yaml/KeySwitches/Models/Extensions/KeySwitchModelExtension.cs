using System.Collections.Generic;
using System.Linq;

using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Models.Aggregations;

namespace KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Models.Extensions
{
    public static class KeySwitchModelExtension
    {
        public static IReadOnlyCollection<KeySwitchModel> Find( this YamlModel me, KeySwitchId keySwitchId )
        {
            return me.KeySwitches.Where( x => x.Id == keySwitchId.Value ).ToList();
        }

        public static IReadOnlyCollection<KeySwitchModel> Find( this YamlModel me, DeveloperName developerName, ProductName productName, InstrumentName instrumentName )
        {
            var d = developerName.Value;
            var p = productName.Value;
            var i = instrumentName.Value;

            return me.KeySwitches.FindAll(
                x =>
                    ( d == DeveloperName.Any.Value  || x.DeveloperName.Contains( d ) ) &&
                    ( p == ProductName.Any.Value    || x.ProductName.Contains( p ) )   &&
                    ( i == InstrumentName.Any.Value || x.InstrumentName.Contains( i ) )
            );
        }

        public static IReadOnlyCollection<KeySwitchModel> Find( this YamlModel me, DeveloperName developerName, ProductName productName )
        {
            var d = developerName.Value;
            var p = productName.Value;

            return me.KeySwitches.FindAll(
                x =>
                    ( d == DeveloperName.Any.Value  || x.DeveloperName.Contains( d ) ) &&
                    ( p == ProductName.Any.Value    || x.ProductName.Contains( p ) )
            );
        }

        public static IReadOnlyCollection<KeySwitchModel> Find( this YamlModel me, DeveloperName developerName )
        {
            var d = developerName.Value;

            return me.KeySwitches.FindAll(
                x =>
                    d == DeveloperName.Any.Value  || x.DeveloperName.Contains( d )
            );
        }

        public static IReadOnlyCollection<KeySwitchModel> Find( this YamlModel me, ProductName productName )
        {
            var p = productName.Value;

            return me.KeySwitches.FindAll(
                x =>
                    p == ProductName.Any.Value    || x.ProductName.Contains( p )
            );
        }

        public static IReadOnlyCollection<KeySwitchModel> Find( this YamlModel me, InstrumentName instrumentName )
        {
            var i = instrumentName.Value;

            return me.KeySwitches.FindAll(
                x =>
                    i == InstrumentName.Any.Value || x.InstrumentName.Contains( i )
            );
        }
    }
}
