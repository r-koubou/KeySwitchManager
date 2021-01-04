using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

using CommandLine;

using Databases.LiteDB.KeySwitches.KeySwitches;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Interactors.KeySwitches.Exporting;
using KeySwitchManager.UseCases.KeySwitches.Exporting;
using KeySwitchManager.Xlsx.KeySwitches.ClosedXml;

using RkHelper.Text;

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

            [Option( 'p', "product" )]
            public string Product { get; set; } = string.Empty;

            [Option( 'f', "database", Required = true )]
            public string DatabasePath { get; set; } = string.Empty;

            [Option( 'o', "output-dir", Required = true )]
            public string OutputPath { get; set; } = string.Empty;
        }

        public int Execute( ICommandOption opt )
        {
            var progress = new[] { "|", "/", "-", "\\" };
            var progressCount = 0;

            var option = (CommandOption)opt;

            var entities = Query( option );

            using var xlsxRepository = new XlsxExportingRepository( new DirectoryPath( option.OutputPath ) );
            var interactor = new ExportingXlsxInteractor( xlsxRepository );
            var task = Task.Run( () => interactor.Execute( new ExportingXlsxRequest( entities ) ) );

            while( !task.IsCompleted )
            {
                Console.Write( $"\rExporting ... {progress[ progressCount ]}" );
                progressCount = ( progressCount + 1 ) % progress.Length;
                Task.Delay( 200 ).Wait();
            }
            Console.WriteLine();

            return task.Result.Result ? 0 : 1;
        }

        private static IReadOnlyCollection<KeySwitch> Query( CommandOption option )
        {
            using var repository = new LiteDbKeySwitchRepository( option.DatabasePath );

            var developerName = IDeveloperNameFactory.Default.Create( option.Developer );

            if( StringHelper.IsEmpty( option.Product ) )
            {
                return repository.Find( developerName );
            }

            return repository.Find( developerName, IProductNameFactory.Default.Create( option.Product ) );
        }
    }
}