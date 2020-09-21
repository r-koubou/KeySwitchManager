using System;

using CommandLine;

using Databases.LiteDB.KeySwitches.KeySwitches;

using KeySwitchManager.Interactors.KeySwitches.Searching;
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
        }

        public int Execute( ICommandOption opt )
        {
            var option = (CommandOption)opt;

            using var repository = new LiteDbKeySwitchRepository( option.DatabasePath );
            var presenter = new ISearchingPresenter.Console();
            var interactor = new SearchingInteractor( repository, presenter );

            var input = new SearchingRequest( option.Developer, option.Product, option.Instrument );

            var response = interactor.Execute( input );

            if( response.FoundCount > 0 )
            {
                foreach( var i in response.Result )
                {
                    Console.Out.WriteLine( $"{i.DeveloperName}, {i.ProductName}, {i.InstrumentName}, {i.LastUpdated}" );
                }
                Console.Out.WriteLine( $"{response.FoundCount} record(s) found" );
            }
            else
            {
                Console.Out.WriteLine( "record not found" );
            }

            return 0;
        }
    }
}