using System;
using System.IO;
using System.Text;

using CommandLine;

using Databases.LiteDB.KeySwitches.KeySwitches;

using KeySwitchManager.Interactors.KeySwitches.Exporting;
using KeySwitchManager.Json.KeySwitches.Translations;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Exporting;

namespace KeySwitchManager.Apps.CLI.Commands
{
    public class Export : ICommand
    {
        [Verb( "export", false, HelpText = "export to generic json format")]
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

            [Option( 'o', "output", Required = true )]
            public string OutputPath { get; set; } = string.Empty;
        }

        public int Execute( ICommandOption opt )
        {
            var option = (CommandOption)opt;

            var repository = new LiteDbKeySwitchRepository( option.DatabasePath );
            var translator = new KeySwitchListListToJsonModelList{ Formatted = true };
            var presenter = new IExportingTextPresenter.Null();
            var interactor = new ExportingJsonInteractor( repository, translator, presenter );

            var input = new ExportingTextRequest( option.Developer, option.Product, option.Instrument );

            Console.WriteLine( $"Developer=\"{option.Developer}\", Product=\"{option.Product}\", Instrument=\"{option.Instrument}\"" );

            var response = interactor.Execute( input );

            if( response.Count > 0 )
            {
                File.WriteAllText( option.OutputPath, response.Text.Value, Encoding.UTF8 );
                Console.WriteLine( $"{response.Count} records exported to {option.OutputPath}" );

                return 0;
            }

            Console.WriteLine( "records not found" );
            return 0;
        }
    }
}