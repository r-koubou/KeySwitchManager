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
            IEnumerable<Articulation> articulations );

        public class Default : IKeySwitchFactory
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
                IEnumerable<Articulation> articulations )
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
                    articulations
                );
            }
        }
    }
}