using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using CommandLine;

using Databases.LiteDB.KeySwitches.KeySwitches;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Interactors.KeySwitches.Exporting;
using KeySwitchManager.UseCases.KeySwitches.Exporting;
using KeySwitchManager.Xlsx.KeySwitches;

namespace KeySwitchManager.CLI.Commands
{
    public class ExportXlsx : ICommand
    {
        [Verb( "export-xlsx", HelpText = "export a xlsx format from database")]
        [SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" )]
        [SuppressMessage( "ReSharper", "ClassNeverInstantiated.Global" )]
        public class CommandOption : ICommandOption
        {
            [Option( 'd', "developer", Required = true)]
            public string Developer { get; set; } = string.Empty;

            [Option( 'p', "product", Required = true )]
            public string Product { get; set; } = string.Empty;

            [Option( 'f', "database", Required = true )]
            public string DatabasePath { get; set; } = string.Empty;

            [Option( 'o', "output", Required = true )]
            public string OutputPath { get; set; } = string.Empty;
            [Option( 'l', "log" )]
            public string LogFilePath { get; set; } = string.Empty;
        }

        public int Execute( ICommandOption opt )
        {
            var option = (CommandOption)opt;

            var entities = Query( option );

            using var xlsxRepository = new XlsxExportingRepository( new FilePath( option.OutputPath ) );
            var interactor = new ExportingXlsxInteractor( xlsxRepository );
            var response = interactor.Execute( new ExportingXlsxRequest( entities ) );

            return response.Result ? 0 : 1;
        }

        private static IReadOnlyCollection<KeySwitch> Query( CommandOption option )
        {
            using var repository = new LiteDbKeySwitchRepository( option.DatabasePath );
            var developerName = new DeveloperName( option.Developer );
            var productName = new ProductName( option.Product );

            return repository.Find( developerName, productName );
        }
    }
}