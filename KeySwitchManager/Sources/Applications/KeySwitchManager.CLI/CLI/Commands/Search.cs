using System;
using System.IO;
using System.Text;

using CommandLine;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructure.Database.LiteDB.KeySwitches;
using KeySwitchManager.Interactor.KeySwitches;
using KeySwitchManager.Storage.Yaml.KeySwitches.Translators;
using KeySwitchManager.UseCase.KeySwitches.Find;

using RkHelper.Text;

namespace KeySwitchManager.CLI.Commands
{
    public class Search
    {
        [Verb( "search", HelpText = "search a data from database by given parameter")]
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

            using var repository = new LiteDbKeySwitchRepository( new FilePath( option.DatabasePath ) );

            var presenter = new IFindPresenter.Console();
            var interactor = new FindInteractor( repository, presenter );

            var request = new FindRequest( option.Developer, option.Product, option.Instrument );

            var response = interactor.Execute( request );
            var outputText = new YamlKeySwitchExportTranslator().Translate( response.Result ).Value;

            if( StringHelper.IsEmpty( option.OutputPath ) )
            {
                Console.Out.WriteLine( $"{outputText}" );
            }
            else
            {
                File.WriteAllText( option.OutputPath, outputText, Encoding.UTF8 );
                Console.Out.WriteLine( $"Output yaml to {option.OutputPath}" );
            }

            Console.Error.WriteLine( $"{response.FoundCount} record(s) found" );

            return 0;
        }
    }
}