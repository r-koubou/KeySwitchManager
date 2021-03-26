using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

using CommandLine;

using Database.LiteDB.KeySwitch.KeySwitch;

using KeySwitchManager.Interactors.KeySwitches.Searching;
using KeySwitchManager.Json.KeySwitch.Translation;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Searching;

using RkHelper.Text;

namespace KeySwitchManager.CLI.Commands
{
    public class Search
    {
        [Verb( "search", HelpText = "search a data from database by given parameter")]
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

            [Option( 'o', "output" )]
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

            if( StringHelper.IsEmpty( option.OutputPath ) )
            {
                Console.Out.WriteLine( $"{response.Text}" );
            }
            else
            {
                File.WriteAllText( option.OutputPath, response.Text.Value, Encoding.UTF8 );
                Console.Out.WriteLine( $"Output json to {option.OutputPath}" );
            }

            Console.Error.WriteLine( $"{response.FoundCount} record(s) found" );

            return 0;
        }
    }
}