using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;

using CommandLine;

using Database.LiteDB.KeySwitch.KeySwitch;

using KeySwitchManager.CLI.Helpers;
using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Interactors.StudioOneKeySwitch.Exporting;
using KeySwitchManager.Presenters.StudioOneKeySwitch;
using KeySwitchManager.UseCases.KeySwitch.StudioOne.Exporting;
using KeySwitchManager.Xml.KeySwitch.StudioOne.Translation;

using RkHelper.IO;

namespace KeySwitchManager.CLI.Commands
{
    public class ExportStudioOneKeySwitch : ICommand
    {
        [Verb( "studio-one", HelpText = "export to Studio One KeySwitch format")]
        [SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" )]
        [SuppressMessage( "ReSharper", "ClassNeverInstantiated.Global" )]
        public class CommandOption : ICommandOption
        {
            [Option( 'd', "developer", Required = true)]
            public string Developer { get; set; } = string.Empty;

            [Option( 'p', "product" )]
            public string Product { get; set; } = string.Empty;

            [Option( 'i', "instrument" )]
            public string Instrument { get; set; } = string.Empty;

            [Option( 'f', "database", Required = true )]
            public string DatabasePath { get; set; } = string.Empty;

            [Option( 'o', "outputdir", Required = true )]
            public string OutputDirectory { get; set; } = string.Empty;
            [Option( 's', "structure-dir" )]
            public bool DirectoryStructure { get; set; } = false;
        }

        public int Execute( ICommandOption opt )
        {
            var option = (CommandOption)opt;

            using var repository = new LiteDbKeySwitchRepository( option.DatabasePath );
            var translator = new KeySwitchToStudioOneKeySwitchModel();
            var presenter = new IExportingStudioOneKeySwitchPresenter.Null();
            var interactor = new ExportingStudioOneKeySwitchInteractor( repository, translator, presenter );

            var input = new ExportingStudioOneKeySwitchRequest( option.Developer, option.Product, option.Instrument );

            Console.WriteLine( $"Developer=\"{option.Developer}\", Product=\"{option.Product}\", Instrument=\"{option.Instrument}\"" );

            var response = interactor.Execute( input );

            if( !response.Elements.Any() )
            {
                Console.WriteLine( "records not found" );
                return 0;
            }

            foreach( var i in response.Elements )
            {
                var outputDirectory = option.OutputDirectory;

                if( option.DirectoryStructure )
                {
                    outputDirectory = EntityDirectoryHelper.CreateDirectoryTree(
                        i.KeySwitch,
                        new DirectoryPath( option.OutputDirectory )
                    ).Path;
                }
                else
                {
                    DirectoryHelper.Create( outputDirectory );
                }

                var prefix = $"{i.KeySwitch.ProductName} {i.KeySwitch.InstrumentName}";
                var path = $"{prefix}.keyswitch";
                path = PathHelper.IncrementPathNameWhenExist( outputDirectory, path );

                Console.Out.WriteLine( $"export to {path}" );
                File.WriteAllText( path, i.XmlText.Value, Encoding.UTF8 );
            }

            return 0;
        }
    }
}