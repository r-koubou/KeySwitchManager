using System.Collections.Generic;
using System.IO;

using CommandLine;

using Databases.LiteDB.KeySwitches.KeySwitches;

using KeySwitchManager.Cli.Commons;
using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Interactors.KeySwitches.Importing.Text;
using KeySwitchManager.Json.KeySwitches.Translations;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Importing.Text;

namespace KeySwitchManager.App.ImportFromJson
{
    class CommandlineOption
    {
        [Option( 'd', "dbfile", Required = true)]
        public string DbFile { get; set; } = string.Empty;

        [Value( 0, MetaName = "input files", Required = true )]
        public IEnumerable<string> InputFiles { get; set; } = new List<string>();
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

            foreach( var i in option.InputFiles )
            {
                using var stream = File.Open( option.DbFile, FileMode.OpenOrCreate );
                using var repository = new LiteDbKeySwitchRepository( stream );

                var json = new PlainText( File.ReadAllText( i ) );
                var input = new ImportingTextRequest( json );
                var presenter = new IImportingTextPresenter.Console();
                var interactor = new ImportingJsonInteractor(
                    repository,
                    presenter,
                    new JsonModelListToKeySwitchList()
                );

                presenter.Present( $"Importing from {i}" );
                var response = interactor.Execute( input );
            }

        }

    }
}