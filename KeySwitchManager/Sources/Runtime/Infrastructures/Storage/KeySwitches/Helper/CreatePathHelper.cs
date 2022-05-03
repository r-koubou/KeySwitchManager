using System.IO;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Values;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper
{
    public static class CreatePathHelper
    {
        public static DirectoryPath CreateDirectoryTree( DeveloperName developerName, ProductName productName, DirectoryPath baseDirectory, params DirectoryPath[] subDirectories )
        {
            var outputDirectory = baseDirectory.Path;

            foreach( var i in subDirectories )
            {
                outputDirectory = Path.Combine( outputDirectory, i.Path );
            }

            outputDirectory = Path.Combine(
                outputDirectory,
                developerName.Value,
                productName.Value
            );

            var result = new DirectoryPath( outputDirectory );
            result.CreateNew();

            return result;

        }

        public static DirectoryPath CreateDirectoryTree( DeveloperName developerName, DirectoryPath baseDirectory, params DirectoryPath[] subDirectories )
        {
            var outputDirectory = baseDirectory.Path;

            foreach( var i in subDirectories )
            {
                outputDirectory = Path.Combine( outputDirectory, i.Path );
            }

            outputDirectory = Path.Combine(
                outputDirectory,
                developerName.Value
            );

            var result = new DirectoryPath( outputDirectory );
            result.CreateNew();

            return result;

        }

        public static DirectoryPath CreateDirectoryTree( KeySwitch keySwitch, DirectoryPath baseDirectory, params DirectoryPath[] subDirectories )
        {
            return CreateDirectoryTree( keySwitch.DeveloperName, keySwitch.ProductName, baseDirectory, subDirectories );
        }

        public static FilePath CreateFilePath( DeveloperName developerName, ProductName productName, InstrumentName instrumentName, string prefix, string suffix, DirectoryPath baseDirectory, params DirectoryPath[] subDirectories )
        {
            var outputDirectory = CreateDirectoryTree( developerName, productName, baseDirectory, subDirectories );

            return new FilePath(
                Path.Combine( outputDirectory.Path, $"{prefix}{instrumentName}{suffix}" )
            );
        }

        public static FilePath CreateFilePath( DeveloperName developerName, ProductName productName, string prefix, string suffix, DirectoryPath baseDirectory, params DirectoryPath[] subDirectories )
        {
            var outputDirectory = CreateDirectoryTree( developerName, baseDirectory, subDirectories );

            return new FilePath(
                Path.Combine( outputDirectory.Path, $"{prefix}{productName}{suffix}" )
            );
        }

        public static FilePath CreateFilePath( DeveloperName developerName, ProductName productName, string suffix, DirectoryPath baseDirectory, params DirectoryPath[] subDirectories )
        {
            return CreateFilePath( developerName, productName, string.Empty, suffix, baseDirectory );
        }

        public static FilePath CreateFilePath( KeySwitch keySwitch, string suffix, DirectoryPath baseDirectory, params DirectoryPath[] subDirectories )
        {
            return CreateFilePath(
                keySwitch.DeveloperName,
                keySwitch.ProductName,
                keySwitch.InstrumentName,
                string.Empty,
                suffix,
                baseDirectory,
                subDirectories
            );
        }

        public static FilePath CreateFilePath( KeySwitch keySwitch, string prefix, string suffix, DirectoryPath baseDirectory, params DirectoryPath[] subDirectories )
        {
            return CreateFilePath(
                keySwitch.DeveloperName,
                keySwitch.ProductName,
                keySwitch.InstrumentName,
                prefix,
                suffix,
                baseDirectory,
                subDirectories
            );
        }
    }
}