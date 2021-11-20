using System;
using System.IO;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches;

namespace KeySwitchManager.Applications.Core.Helpers
{
    public static class KeySwitchFileReaderFactory
    {
        public static IKeySwitchReader Create( string filePath )
        {
            var path = filePath.ToLower();

            if( path.EndsWith( ".xlsx" ) )
            {
                throw new NotImplementedException( $"{filePath}({nameof( IKeySwitchReader )}) for .xlsx is not ready" );
            }

            if( path.EndsWith( ".yaml" ) || path.EndsWith( ".yml" ) )
            {
                return new YamlKeySwitchReader( File.OpenRead( filePath ) );
            }

            throw new ArgumentException( $"{filePath} is unknown file format" );

        }
    }
}
