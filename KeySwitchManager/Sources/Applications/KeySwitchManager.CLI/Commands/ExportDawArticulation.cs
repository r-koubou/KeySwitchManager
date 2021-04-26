using System;
using System.Linq;

using CommandLine;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Helpers;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructure.Database.LiteDB.KeySwitches;
using KeySwitchManager.Interactor.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export.Daw;

namespace KeySwitchManager.CLI.Commands
{
    public abstract class ExportDawArticulation : ICommand
    {
        public class CommandOption : ICommandOption
        {
            [Option( 'd', "developer" )]
            public string Developer { get; set; } = string.Empty;

            [Option( 'p', "product" )]
            public string Product { get; set; } = string.Empty;

            [Option( 'i', "instrument" )]
            public string Instrument { get; set; } = string.Empty;

            [Option( 'f', "database", Required = true )]
            public string DatabasePath { get; set; } = string.Empty;

            [Option( 'o', "outputdir", Required = true )]
            public string OutputDirectory { get; set; } = string.Empty;
        }

        public virtual int Execute( ICommandOption opt )
        {
            var option = (CommandOption)opt;

            using var repository = new LiteDbKeySwitchRepository( new FilePath( option.DatabasePath ) );

            var keySwitches = SearchHelper.Search( repository, option.Developer, option.Product, option.Instrument );

            if( !keySwitches.Any() )
            {
                Console.WriteLine( "records not found" );
                return 0;
            }

            using var outputRepository = CreateOutputRepository( new DirectoryPath( option.OutputDirectory ) );
            var interactor = new DawExportInteractor( repository, outputRepository, new IDawExportPresenter.Console() );

            var request = new DawExportRequest( option.Developer, option.Product, option.Instrument );
            _ = interactor.Execute( request );

            return 0;
        }

        public abstract IKeySwitchRepository CreateOutputRepository( DirectoryPath outputDirectory );
    }
}