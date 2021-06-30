using System;
using System.Linq;

using CommandLine;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Helpers;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Database.LiteDB.KeySwitches;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export.Daw;

namespace KeySwitchManager.Applications.CLI.Commands
{
    public abstract class ExportDawArticulation : ICommand
    {
        public class CommandOption : ICommandOption
        {
            [Option( 'q', "quiet" )]
            public bool Quiet { get; set; } = false;

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
                if( !option.Quiet )
                {
                    Console.WriteLine( "records not found" );
                }

                return 0;
            }

            using var outputRepository = CreateOutputRepository( new DirectoryPath( option.OutputDirectory ) );

            IExportDawPresenter presenter = option.Quiet ?
                new IExportDawPresenter.Null() :
                new IExportDawPresenter.Console();

            var interactor = new ExportDawInteractor( repository, outputRepository, presenter );

            var request = new ExportDawRequest( option.Developer, option.Product, option.Instrument );
            var response = interactor.Execute( request );

            return response.Result ? 0 : 1;
        }

        public abstract IKeySwitchRepository CreateOutputRepository( DirectoryPath outputDirectory );
    }
}