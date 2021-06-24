using System;

using CommandLine;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructure.Database.LiteDB.KeySwitches;
using KeySwitchManager.Interactor.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Delete;

namespace KeySwitchManager.CLI.Commands
{
    public class Delete : ICommand
    {
        [Verb( "delete", false, HelpText = "delete records from database")]
        public class CommandOption : ICommandOption
        {
            [Option( 'd', "developer", Required = true )]
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

            using var repository = new LiteDbKeySwitchRepository( new FilePath( option.DatabasePath ) );
            var interactor = new DeleteInteractor( repository );
            var request = new DeleteRequest( option.Developer, option.Product, option.Instrument );

            Console.WriteLine( $"Developer=\"{option.Developer}\", Product=\"{option.Product}\", Instrument=\"{option.Instrument}\"" );

            var response = interactor.Execute( request );

            if( response.RemovedCount > 0 )
            {
                Console.WriteLine( $"{response.RemovedCount} records deleted from database({option.DatabasePath})" );
                return 0;
            }

            Console.WriteLine( "records not found" );
            return 0;
        }
    }
}