using System;
using System.Collections.Generic;

using ArticulationManager.Common.Utilities;
using ArticulationManager.Domain.Commons;
using ArticulationManager.Domain.KeySwitches.Aggregate;
using ArticulationManager.Domain.KeySwitches.Value;
using ArticulationManager.Domain.Services;

namespace ArticulationManager.Domain.KeySwitches
{
    public interface IKeySwitchFactory
    {
        public KeySwitch Create(
            Guid id,
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
                    EntityDateTimeService.FromDateTime( created ),
                    EntityDateTimeService.FromDateTime( lastUpdated ),
                    new DeveloperName( developerName ),
                    new ProductName( productName ),
                    new InstrumentName( instrumentName ),
                    articulations
                );
            }
        }
    }
}