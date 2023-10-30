using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper;
using KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.StudioOne.Models;
using KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.StudioOne.Translators;
using KeySwitchManager.UseCase.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

using RkHelper.Text.Xml;

namespace KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.StudioOne
{
    public class StudioOneExportContentFactory : IExportContentFactory
    {
        public Task<IContent> CreateAsync( IReadOnlyCollection<KeySwitch> keySwitches, CancellationToken cancellationToken = default )
        {
            KeySwitchValidateHelper.ValidateNotEmpty( keySwitches );

            var source = keySwitches.First();

            var productName = source.ProductName;
            var rootElement = StudioOneExportTranslator.TranslateRootElement( productName );

            Translate( rootElement, keySwitches );

            var xmlText = XmlHelper.ToXmlString( rootElement );

            return Task.FromResult<IContent>( new StringContent( xmlText ) );
        }

        private static void Translate(
            RootElement rootElement,
            IReadOnlyCollection<KeySwitch> keySwitches )
        {
            var assignId = 0;
            var count = keySwitches.Count();

            foreach( var x in keySwitches )
            {
                var elementAttributes = StudioOneExportTranslator.TranslateElementAttributes( x.Articulations, ref assignId );

                // Create a folder element if count more than 2
                if( count >= 2 )
                {
                    var folder = new AttributeElement
                    {
                        Folder = "1",
                        Name   = x.InstrumentName.Value
                    };

                    folder.Children.AddRange( elementAttributes );
                    rootElement.AttributeElements.Add( folder );
                }
                else
                {
                    rootElement.AttributeElements.AddRange( elementAttributes );
                }
            }
        }
    }
}
