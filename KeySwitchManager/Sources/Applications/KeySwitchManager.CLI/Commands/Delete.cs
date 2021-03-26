using System;
using System.Diagnostics.CodeAnalysis;

using CommandLine;

using Database.LiteDB.KeySwitches;

using KeySwitchManager.Interactors.KeySwitches.Removing;
using KeySwitchManager.Json.KeySwitches.Translators;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Removing;

namespace KeySwitchManager.CLI.Commands
{
    public class Delete : ICommand
    {
        [Verb( "delete", false, HelpText = "delete records from database")]
        [SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" )]
        [SuppressMessage( "ReSharper", "ClassNeverInstantiated.Global" )]
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

            using var repository = new LiteDbKeySwitchRepository( option.DatabasePath );
            var translator = new KeySwitchListListToJsonModelList{ Formatted = true };
            var presenter =  new IRemovingPresenter.Null();
            var interactor = new RemovingInteractor( repository, presenter );

            var input = new RemovingRequest( option.Developer, option.Product, option.Instrument );

            Console.WriteLine( $"Developer=\"{option.Developer}\", Product=\"{option.Product}\", Instrument=\"{option.Instrument}\"" );

            var response = interactor.Execute( input );

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