using System;
using System.IO;
using System.Text;

using CommandLine;

using Databases.LiteDB.KeySwitches.KeySwitches;

using KeySwitchManager.Interactors.KeySwitches.Searching;
using KeySwitchManager.Json.KeySwitches.Translations;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Searching;

namespace KeySwitchManager.Apps.CLI.Commands
{
    public class Search
    {
        [Verb( "search", false, HelpText = "search a data from database by given parameter")]
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

            using var repository = new LiteDbKeySwitchRepository( option.DatabasePath );
            var translator = new KeySwitchListListToJsonModelList
            {
                Formatted = true
            };

            var presenter = new ISearchingPresenter.Console();
            var interactor = new SearchingInteractor( repository, translator, presenter );

            var input = new SearchingRequest( option.Developer, option.Product, option.Instrument );

            var response = interactor.Execute( input );

            File.WriteAllText( option.OutputPath, response.Text.Value, Encoding.UTF8 );

            Console.Out.WriteLine( $"Output json to {option.OutputPath}" );
            Console.Out.WriteLine( $"{response.FoundCount} record(s) found" );

            return 0;
        }
    }
}