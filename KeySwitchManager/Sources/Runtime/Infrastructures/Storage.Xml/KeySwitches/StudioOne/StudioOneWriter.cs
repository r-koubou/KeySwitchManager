using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Helpers;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper;
using KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.StudioOne.Models;
using KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.StudioOne.Translators;

using RkHelper.Text.Xml;

namespace KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.StudioOne
{
    public class StudioOneWriter : IKeySwitchWriter
    {
        private const string Suffix = ".keyswitch";

        private DirectoryPath OutputDirectory { get; }
        private Encoding FileEncoding { get; }
        public bool LeaveOpen => false;

        public StudioOneWriter( DirectoryPath outputDirectory ) : this( outputDirectory, Encoding.UTF8 ) {}

        public StudioOneWriter( DirectoryPath outputDirectory, Encoding filEncoding )
        {
            OutputDirectory = outputDirectory;
            FileEncoding    = filEncoding;
        }

        public void Dispose() {}

        public void Write( IReadOnlyCollection<KeySwitch> keySwitches, IObserver<string>? logging = null )
        {
            if( !keySwitches.Any() )
            {
                throw new ArgumentException( $"{nameof( keySwitches )} is empty" );
            }

            var group = KeySwitchHelper.GroupBy( keySwitches );

            foreach( var ((developerName, productName), x) in group )
            {
                var rootElement = StudioOneExportTranslator.TranslateRootElement( productName );

                Translate( rootElement, developerName, productName, x, logging );

                var filePath = CreatePathHelper.CreateFilePath( developerName, productName, Suffix, OutputDirectory );
                using var stream = filePath.OpenStream( FileMode.Create, FileAccess.ReadWrite );
                using var writer = new StreamWriter( stream, FileEncoding, IKeySwitchWriter.DefaultStreamWriterBufferSize, LeaveOpen );

                var xmlText = XmlHelper.ToXmlString( rootElement );
                writer.WriteLine( xmlText );
            }
        }

        private static void Translate(
            RootElement rootElement,
            DeveloperName developerName,
            ProductName productName,
            IReadOnlyCollection<KeySwitch> keySwitches,
            IObserver<string>? logging )
        {
            var count = keySwitches.Count();

            logging?.OnNext( $"{developerName} | {productName}" );

            foreach( var x in keySwitches )
            {
                var elementAttributes = StudioOneExportTranslator.TranslateElementAttributes( x.Articulations );

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
