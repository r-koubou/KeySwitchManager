using System.Collections.Generic;
using System.IO;
using System.Linq;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Commons.Helpers;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Infrastructure.Storage.KeySwitches;
using KeySwitchManager.Infrastructure.Storage.Spreadsheet.ClosedXml.KeySwitches.Helper;

namespace KeySwitchManager.Infrastructure.Storage.Spreadsheet.ClosedXml.KeySwitches
{
    public class ClosedXmlFileSaveRepository : SaveOnlyKeySwitchFileRepository
    {
        private DirectoryPath TargetDirectory { get; }

        public ClosedXmlFileSaveRepository( DirectoryPath targetDirectory ) : base( targetDirectory )
        {
            TargetDirectory = targetDirectory;
        }

        #region Save
        public sealed override int Flush()
        {
            if( !KeySwitches.Any() )
            {
                return 0;
            }

            var productList = new Dictionary<(DeveloperName,ProductName), List<KeySwitch>>();

            foreach( var keySwitch in KeySwitches )
            {
                var key = ( keySwitch.DeveloperName, keySwitch.ProductName );
                if( !productList.ContainsKey( key ) )
                {
                    productList[ key ] = new List<KeySwitch>();
                }
                productList[ key ].Add( keySwitch );
            }

            var saved = 0;
            TargetDirectory.CreateNew();

            foreach( var key in productList.Keys )
            {
                var developerName = key.Item1;
                var productName = key.Item2;
                var instruments = productList[ key ].OrderBy( x => x.InstrumentName.Value ).ToList();

                var outputDir = PathHelper.Combine(
                    TargetDirectory,
                    new DirectoryPath( developerName.Value )
                );

                var outputFilePath = Path.Combine( outputDir.Path, $"{productName}.xlsx" );
                outputDir.CreateNew();

                XlsxWorkBookWriter.Write(
                    instruments,
                    new FilePath( outputFilePath )
                );
                saved++;
            }

            return saved;

        }
        #endregion

    }
}
