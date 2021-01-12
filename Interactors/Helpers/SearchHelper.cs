using System;
using System.Collections.Generic;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Gateways.KeySwitches;

using RkHelper.Text;

namespace KeySwitchManager.Interactors.Helpers
{
    public static class SearchHelper
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
            if( !StringHelper.IsEmpty( developerName, productName, instrumentName ) )
            {
                return new List<KeySwitch>(
                    repository.Find(
                        IDeveloperNameFactory.Default.Create( developerName ),
                        IProductNameFactory.Default.Create( productName ),
                        IInstrumentNameFactory.Default.Create( instrumentName ) )
                );
            }
            #endregion

            #region By Developer, Product
            if( !StringHelper.IsEmpty( developerName, productName ) )
            {
                return new List<KeySwitch>(
                    repository.Find(
                        IDeveloperNameFactory.Default.Create( developerName ),
                        IProductNameFactory.Default.Create( productName )
                    )
                );
            }
            #endregion

            #region By Developer
            if( !StringHelper.IsEmpty( developerName ) )
            {
                return new List<KeySwitch>(
                    repository.Find(
                        IDeveloperNameFactory.Default.Create( developerName )
                    )
                );
            }
            #endregion

            return new List<KeySwitch>();
        }
    }
}