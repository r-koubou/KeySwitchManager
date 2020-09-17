using System.IO;

using CommandLine;

using KeySwitchManager.Cli.Commons;
using KeySwitchManager.Databases.LiteDB.KeySwitches;
using KeySwitchManager.Interactors.KeySwitches.Removing;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Removing;

namespace KeySwitchManager.App.Remove
{
    class CommandlineOption
    {
        [Option( 'd', "developer", Required = true )]
        public string DeveloperName { get; set; } = string.Empty;

        [Option( 'p', "product", Required = true )]
        public string ProductName { get; set; } = string.Empty;

        [Option( 'i', "instrument", Required = true )]
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

            var input = new RemovingRequest( option.DeveloperName, option.ProductName, option.InstrumentName );
            var presenter =  new IRemovingPresenter.Console();
            var interactor = new RemovingInteractor(
                repository,
                presenter
            );

            interactor.Execute( input );
        }

    }
}