using System;
using System.IO;
using System.Linq;
using System.Text;

using CommandLine;

using Databases.LiteDB.KeySwitches.KeySwitches;

using KeySwitchManager.Common.IO;
using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.Services;
using KeySwitchManager.Interactors.VstExpressionMap.Exporting;
using KeySwitchManager.Presenters.VstExpressionMap;
using KeySwitchManager.UseCases.VstExpressionMap.Exporting;
using KeySwitchManager.Xml.VstExpressionMap.Translations;

namespace KeySwitchManager.CLI.Commands
{
    public class ExportVstExpressionMap : ICommand
    {
        [Verb( "vst-expressionmap", false, HelpText = "export to VST Expression Map format")]
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
            var translator = new KeySwitchToVstExpressionMapModel();
            var presenter = new IExportingVstExpressionMapPresenter.Null();
            var interactor = new ExportingVstExpressionMapInteractor( repository, translator, presenter );

            var input = new ExportingVstExpressionMapRequest( option.Developer, option.Product, option.Instrument );

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
                    outputDirectory = EntityDirectoryService.CreateDirectoryTree(
                        i.KeySwitch,
                        new DirectoryPath( option.OutputDirectory ),
                        new DirectoryPath( "VST Expressionmap" )
                    ).Path;
                }

                var prefix = $"{i.KeySwitch.ProductName} {i.KeySwitch.InstrumentName}";
                var path = Path.Combine( outputDirectory, prefix + ".expressionmap" );
                path = PathUtility.GenerateFilePathWhenExist( path, outputDirectory );

                Console.Out.WriteLine( $"export to {path}" );
                File.WriteAllText( path, i.XmlText.Value, Encoding.UTF8 );
            }

            return 0;
        }
    }
}