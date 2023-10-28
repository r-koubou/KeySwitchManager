using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Json.KeySwitches.Cakewalk.Translators;
using KeySwitchManager.UseCase.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.Json.KeySwitches.Cakewalk
{
    public class CakewalkExportContentFactory : IExportContentFactory
    {
        public Task<IContent> CreateAsync( IReadOnlyCollection<KeySwitch> keySwitches )
        {
            if( !keySwitches.Any() )
            {
                throw new ArgumentException( $"{nameof( keySwitches )} is empty" );
            }

            if( keySwitches.Count >= 2 )
            {
                throw new ArgumentException( $"{nameof( keySwitches )} has 1 element only" );
            }

            var source = keySwitches.First();

            // TODO すべての要素を束ねた 1 JSONファイルにしたい(Cakewalkは保持できる)
            var jsonText = new CakewalkExportTranslator( true ).Translate( source );

            return Task.FromResult<IContent>( new StringContent( jsonText.Value ) );
        }
    }
}
