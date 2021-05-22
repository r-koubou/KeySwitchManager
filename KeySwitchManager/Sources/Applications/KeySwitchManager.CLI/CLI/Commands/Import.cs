using System.Diagnostics.CodeAnalysis;

using CommandLine;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructure.Database.LiteDB.KeySwitches;
using KeySwitchManager.Interactor.KeySwitches;
using KeySwitchManager.Storage.Yaml.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Import.Text;

namespace KeySwitchManager.CLI.Commands
{
    public class Import : ICommand
    {
        [Verb( "import", HelpText = "import a yaml to database")]
        [SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" )]
        [SuppressMessage( "ReSharper", "ClassNeverInstantiated.Global" )]
        public class CommandOption : ICommandOption
        {
            [Option( 'f', "database", Required = true )]
            public string DatabasePath { get; set; } = string.Empty;

            [Option( 'i', "input", Required = true )]
            public string InputPath { get; set; } = string.Empty;
        }

        public int Execute( ICommandOption opt )
        {
            var option = (CommandOption)opt;

            using var repository = new LiteDbKeySwitchRepository( new FilePath( option.DatabasePath ) );
            using var inputRepository = new YamlKeySwitchFileRepository( new FilePath( option.InputPath ), true );

            var presenter = new ITextImportPresenter.Console();
            var interactor = new TextImportInteractor( repository, inputRepository, presenter );

            var request = new TextImportRequest();

            _ = interactor.Execute( request );

            return 0;
        }
    }
}