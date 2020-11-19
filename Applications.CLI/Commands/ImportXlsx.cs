using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

using CommandLine;

using Databases.LiteDB.KeySwitches.KeySwitches;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Interactors.KeySwitches.Importing.Xlsx;
using KeySwitchManager.Json.KeySwitches.Translations;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Importing.Xlsx;
using KeySwitchManager.Xlsx.KeySwitches.Translators;

namespace KeySwitchManager.CLI.Commands
{
    public class ImportXlsx : ICommand
    {
        [Verb( "import-xlsx", HelpText = "import a xlsx to database")]
        [SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" )]
        [SuppressMessage( "ReSharper", "ClassNeverInstantiated.Global" )]
        public class CommandOption : ICommandOption
        {
            [Option( 'a', "author" )]
            public string Author { get; set; } = string.Empty;

            [Option( 'n', "description" )]
            public string Description { get; set; } = string.Empty;

            [Option( 'd', "developer", Required = true)]
            public string Developer { get; set; } = string.Empty;

            [Option( 'p', "product", Required = true )]
            public string Product { get; set; } = string.Empty;

            [Option( 'f', "database", Required = true )]
            public string DatabasePath { get; set; } = string.Empty;

            [Option( 'i', "input", Required = true )]
            public string InputPath { get; set; } = string.Empty;
            [Option( 'l', "log" )]
            public string LogFilePath { get; set; } = string.Empty;
        }

        public int Execute( ICommandOption opt )
        {
            var option = (CommandOption)opt;

            using var repository = new LiteDbKeySwitchRepository( option.DatabasePath );
            var translator = new XlsxWorkbookToKeySwitchList(
                option.Developer,
                option.Product,
                option.Author,
                option.Description
            );
            var presenter = new IImportingXlsxPresenter.Console();
            var interactor = new ImportingXlsxInteractor( repository, translator, presenter );

            var input = new ImportingXlsxRequest( new FilePath( option.InputPath ) );

            var response = interactor.Execute( input );
            OutputToJson( response.Imported, option );

            return 0;
        }

        private void OutputToJson( IReadOnlyCollection<KeySwitch> entities, CommandOption option )
        {
            var translator = new KeySwitchListListToJsonModelList
            {
                Formatted = true
            };

            var json = translator.Translate( entities );

            if( !string.IsNullOrEmpty( option.LogFilePath ) )
            {
                File.WriteAllText( option.LogFilePath, json.Value );
            }
        }
    }
}