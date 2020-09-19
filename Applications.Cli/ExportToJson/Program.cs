
using System;
using System.IO;

using CommandLine;

using Databases.LiteDB.KeySwitches.KeySwitches;

using KeySwitchManager.Cli.Commons;
using KeySwitchManager.Interactors.KeySwitches.Exporting.Text;
using KeySwitchManager.Json.KeySwitches.Translations;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Exporting.Text;

namespace KeySwitchManager.App.ExportToJson
{
    class CommandlineOption
    {
        [Option( 'd', "developer")]
        public string DeveloperName { get; set; } = string.Empty;

        [Option( 'p', "product")]
        public string ProductName { get; set; } = string.Empty;

        [Option( 'i', "instrument")]
        public string InstrumentName { get; set; } = string.Empty;

        [Value( 0, MetaName = "input DB file", Required = true )]
        public string DbFile { get; set; } = string.Empty!;
    }

    public class Program
    {
        public static void Main( string[] args )
        {
            var parser = new CliArgumentsParser<CommandlineOption>( args );

            if( !parser.ParsedArguments )
            {
                return;
            }

            var option = parser.Option;

            using var stream = File.Open( option.DbFile, FileMode.OpenOrCreate );
            using var repository = new LiteDbKeySwitchRepository( stream );

            var input = new ExportingTextRequest( option.DeveloperName, option.ProductName );
            var presenter =  new IExportingTextPresenter.Console();
            var interactor = new ExportingJsonInteractor(
                repository,
                new KeySwitchListListToJsonModelList
                {
                    Formatted = true
                },
                presenter
            );

            var response = interactor.Execute( input );

            if( response.Found )
            {
                Console.WriteLine( response.Text );
            }
        }
    }
}