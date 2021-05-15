using System;
using System.IO;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.Infrastructure.Storage.KeySwitches.Helper
{
    public static class CreatePathHelper
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

            var result =new DirectoryPath( outputDirectory );
            result.CreateNew();

            return result;
        }

        public static FilePath CreateFilePath( KeySwitch keySwitch, string suffix, DirectoryPath baseDirectory, params DirectoryPath[] subDirectories )
        {
            return CreateFilePath( keySwitch, string.Empty, suffix, baseDirectory, subDirectories );
        }

        public static FilePath CreateFilePath( KeySwitch keySwitch, string prefix, string suffix, DirectoryPath baseDirectory, params DirectoryPath[] subDirectories )
        {
            var outputDirectory = CreateDirectoryTree( keySwitch, baseDirectory, subDirectories );

            return new FilePath(
                Path.Combine( outputDirectory.Path, $"{prefix}{keySwitch.InstrumentName}{suffix}" )
            );
        }

    }
}