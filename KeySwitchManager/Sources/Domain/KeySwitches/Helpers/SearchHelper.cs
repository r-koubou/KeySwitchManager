using System;
using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Values;

using RkHelper.Text;

namespace KeySwitchManager.Domain.KeySwitches.Helpers
{
    public static class SearchHelper
    {
        #region Search
        public static IReadOnlyCollection<KeySwitch> Search(
            IKeySwitchRepository repository,
            string developerName = "",
            string productName = "",
            string instrumentName = "" )
        {
            return Search( repository, default, developerName, productName, instrumentName );
        }

        public static IReadOnlyCollection<KeySwitch> Search(
            IKeySwitchRepository repository,
            Guid guid = default,
            string developerName = "",
            string productName = "",
            string instrumentName = "" )
        {
            #region By Guid
            if( guid != default )
            {
                return new List<KeySwitch>( repository.Find( new KeySwitchId( guid ) ) );
            }
            #endregion

            #region By Developer, Product, Instrument
            if( !StringHelper.IsEmpty( developerName, productName, instrumentName ) )
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
            if( !StringHelper.IsEmpty( developerName, productName ) )
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
            if( !StringHelper.IsEmpty( developerName ) )
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

        public static IReadOnlyCollection<KeySwitch> Search(
            IKeySwitchRepository repository,
            KeySwitchInfo info )
        {
            return Search(
                repository,
                info.DeveloperName,
                info.ProductName
            );
        }

        #endregion
    }
}