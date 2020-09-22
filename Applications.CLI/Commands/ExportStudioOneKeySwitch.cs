using System;
using System.IO;
using System.Linq;
using System.Text;

using CommandLine;

using Databases.LiteDB.KeySwitches.KeySwitches;

using KeySwitchManager.Common.IO;
using KeySwitchManager.Interactors.StudioOneKeySwitch.Exporting;
using KeySwitchManager.Presenters.StudioOneKeySwitch;
using KeySwitchManager.UseCases.StudioOneKeySwitch.Exporting;
using KeySwitchManager.Xml.StudioOne.KeySwitch.Translations;

namespace KeySwitchManager.Apps.CLI.Commands
{
    public class ExportStudioOneKeySwitch : ICommand
    {
        [Verb( "studio-one", false, HelpText = "export to Studio One KeySwitch format")]
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

            [Option( 'o', "output directory", Required = true )]
            public string OutputDirectory { get; set; } = string.Empty;
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

            if( response.Elements.Any() )
            {
                if( !Directory.Exists( option.OutputDirectory ) )
                {
                    Directory.CreateDirectory( option.OutputDirectory );
                }

                foreach( var i in response.Elements )
                {
                    var path = Path.Combine( option.OutputDirectory, i.KeySwitch.InstrumentName + ".keyswitch" );
                    path = PathUtility.GenerateFilePathWhenExist( path, option.OutputDirectory );

                    Console.Out.WriteLine( $"export to {path}" );
                    File.WriteAllText( path, i.XmlText.Value, Encoding.UTF8 );
                }

                return 0;
            }

            Console.WriteLine( "records not found" );
            return 0;
        }
    }
}