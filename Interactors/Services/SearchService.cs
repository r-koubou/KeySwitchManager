using System;
using System.Collections.Generic;

using KeySwitchManager.Common.Text;
using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Gateways.KeySwitches;

namespace KeySwitchManager.Interactors.Services
{
    public static class SearchService
    {
        public static ICollection<KeySwitch> Search(
            IKeySwitchRepository repository,
            Guid guid = default,
            string developerName = "",
            string productName = "",
            string instrumentName = "" )
        {
            #region By Guid
            if( guid != default )
            {
                return new List<KeySwitch>( repository.Find( new EntityGuid( guid ) ) );
            }
            #endregion

            #region By Developer, Product, Instrument
            if( !StringHelper.IsNullOrTrimEmpty( developerName, productName, instrumentName ) )
            {
                return new List<KeySwitch>(
                    repository.Find(
                        new DeveloperName( developerName ),
                        new ProductName( productName ),
                        new InstrumentName( instrumentName ) )
                );
            }
            #endregion

            #region By Developer, Product
            if( !StringHelper.IsNullOrTrimEmpty( developerName, productName ) )
            {
                return new List<KeySwitch>(
                    repository.Find(
                        new DeveloperName( developerName ),
                        new ProductName( productName )
                    )
                );
            }
            #endregion

            #region By Developer
            if( !StringHelper.IsNullOrTrimEmpty( developerName ) )
            {
                return new List<KeySwitch>(
                    repository.Find(
                        new DeveloperName( developerName )
                    )
                );
            }
            #endregion

            return new List<KeySwitch>();
        }
    }
}