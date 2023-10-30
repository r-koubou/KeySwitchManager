using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
            => SearchAsync( repository, developerName, productName, instrumentName ).GetAwaiter().GetResult();

        public static async Task<IReadOnlyCollection<KeySwitch>> SearchAsync(
            IKeySwitchRepository repository,
            string developerName = "",
            string productName = "",
            string instrumentName = "",
            CancellationToken cancellationToken = default )
        {
            return await SearchAsync( repository, default, developerName, productName, instrumentName );
        }

        public static IReadOnlyCollection<KeySwitch> Search(
            IKeySwitchRepository repository,
            Guid guid = default,
            string developerName = "",
            string productName = "",
            string instrumentName = "" )
            => SearchAsync( repository, guid, developerName, productName, instrumentName ).GetAwaiter().GetResult();

        public static async Task<IReadOnlyCollection<KeySwitch>> SearchAsync(
            IKeySwitchRepository repository,
            Guid guid = default,
            string developerName = "",
            string productName = "",
            string instrumentName = "",
            CancellationToken cancellationToken = default )
        {
            #region By Guid
            if( guid != default )
            {
                return new List<KeySwitch>( await repository.FindAsync( new KeySwitchId( guid ), cancellationToken ) );
            }
            #endregion

            #region By Developer, Product, Instrument
            if( !StringHelper.IsEmpty( developerName, productName, instrumentName ) )
            {
                return new List<KeySwitch>(
                    await repository.FindAsync(
                        new DeveloperName( developerName ),
                        new ProductName( productName ),
                        new InstrumentName( instrumentName ), cancellationToken )
                );
            }
            #endregion

            #region By Developer, Product
            if( !StringHelper.IsEmpty( developerName, productName ) )
            {
                return new List<KeySwitch>(
                    await repository.FindAsync(
                        new DeveloperName( developerName ),
                        new ProductName( productName ),
                        cancellationToken
                    )
                );
            }
            #endregion

            #region By Developer
            if( !StringHelper.IsEmpty( developerName ) )
            {
                return new List<KeySwitch>(
                    await repository.FindAsync(
                        new DeveloperName( developerName ),
                        cancellationToken
                    )
                );
            }
            #endregion

            // All
            return new List<KeySwitch>( await repository.FindAllAsync( cancellationToken ) );
        }

        public static IReadOnlyCollection<KeySwitch> Search(
            IKeySwitchRepository repository,
            KeySwitchInfo info )
            => SearchAsync( repository, info ).GetAwaiter().GetResult();

        public static async Task<IReadOnlyCollection<KeySwitch>> SearchAsync(
            IKeySwitchRepository repository,
            KeySwitchInfo info )
        {
            return await SearchAsync(
                repository,
                info.DeveloperName,
                info.ProductName
            );
        }

        #endregion
    }
}
