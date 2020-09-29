using System;
using System.Collections.Generic;

using KeySwitchManager.Common.Utilities;
using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.KeySwitches.Value;

namespace KeySwitchManager.Domain.KeySwitches
{
    public interface IKeySwitchFactory
    {
        public KeySwitch Create(
            Guid id,
            string author,
            string description,
            DateTime created,
            DateTime lastUpdated,
            string developerName,
            string productName,
            string instrumentName,
            IReadOnlyCollection<Articulation> articulations,
            IReadOnlyDictionary<string, string> extraData );

        public static IKeySwitchFactory Default => new DefaultFactory();

        private class DefaultFactory : IKeySwitchFactory
        {
            public KeySwitch Create(
                Guid id,
                string author,
                string description,
                DateTime created,
                DateTime lastUpdated,
                string developerName,
                string productName,
                string instrumentName,
                IReadOnlyCollection<Articulation> articulations,
                IReadOnlyDictionary<string, string> extraData )
            {
                created     = DateTimeHelper.ToUtc( created );
                lastUpdated = DateTimeHelper.ToUtc( lastUpdated );

                return new KeySwitch(
                    new EntityGuid( id ),
                    new Author( author ),
                    new Description( description ),
                    new EntityDateTime( created ),
                    new EntityDateTime( lastUpdated ),
                    new DeveloperName( developerName ),
                    new ProductName( productName ),
                    new InstrumentName( instrumentName ),
                    articulations,
                    IExtraDataFactory.Default.Create( extraData )
                );
            }
        }
    }
}