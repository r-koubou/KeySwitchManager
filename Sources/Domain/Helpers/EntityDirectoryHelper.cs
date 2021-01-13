using System.IO;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;

namespace KeySwitchManager.Domain.Helpers
{
    public static class EntityDirectoryHelper
    {
        public static DirectoryPath CreateDirectoryTree( KeySwitch keySwitch, DirectoryPath baseDirectory, params DirectoryPath[] subDirectories )
        {
            var outputDirectory = baseDirectory.Path;

            foreach( var i in subDirectories )
            {
                outputDirectory = Path.Combine( outputDirectory, i.Path );
            }

            outputDirectory = Path.Combine(
                outputDirectory,
                keySwitch.DeveloperName.Value,
                keySwitch.ProductName.Value
            );

            if( !Directory.Exists( outputDirectory ) )
            {
                Directory.CreateDirectory( outputDirectory );
            }

            return new DirectoryPath( outputDirectory );
        }
    }
}