using System;
using System.Collections.Generic;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Value;

using RkHelper.Text;

namespace KeySwitchManager.Gateways.KeySwitch.Helper
{
    public static class SearchHelper
    {
        public static ICollection<Domain.KeySwitches.KeySwitch> Search(
            IKeySwitchRepository repository,
            Guid guid = default,
            string developerName = "",
            string productName = "",
            string instrumentName = "" )
        {
            #region By Guid
            if( guid != default )
            {
                return new List<Domain.KeySwitches.KeySwitch>( repository.Find( new EntityGuid( guid ) ) );
            }
            #endregion

            #region By Developer, Product, Instrument
            if( !StringHelper.IsEmpty( developerName, productName, instrumentName ) )
            {
                return new List<Domain.KeySwitches.KeySwitch>(
                    repository.Find(
                        new DeveloperName( developerName ),
                        new ProductName( productName ),
                        new InstrumentName( instrumentName ) )
                );
            }
            #endregion

            #region By Developer, Product
            if( !StringHelper.IsEmpty( developerName, productName ) )
            {
                return new List<Domain.KeySwitches.KeySwitch>(
                    repository.Find(
                        new DeveloperName( developerName ),
                        new ProductName( productName )
                    )
                );
            }
            #endregion

            #region By Developer
            if( !StringHelper.IsEmpty( developerName ) )
            {
                return new List<Domain.KeySwitches.KeySwitch>(
                    repository.Find(
                        new DeveloperName( developerName )
                    )
                );
            }
            #endregion

            return new List<Domain.KeySwitches.KeySwitch>();
        }
    }
}