using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

using CommandLine;

using Database.LiteDB.KeySwitches;

using KeySwitchManager.CLI.Helpers;
using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Interactors.KeySwitches.Cakewalk.Exporting;
using KeySwitchManager.Json.KeySwitches.Cakewalk.Translators;
using KeySwitchManager.Presenters.KeySwitch.Cakewalk;
using KeySwitchManager.UseCases.KeySwitches.Cakewalk.Exporting;

using RkHelper.IO;

namespace KeySwitchManager.CLI.Commands
{
    public class ExportCakewalkArticulation : ICommand
    {
        [Verb( "cakewalk", HelpText = "export to Cakewalk Articulation format")]
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

            [Option( 'o', "outputdir", Required = true )]
            public string OutputDirectory { get; set; } = string.Empty;
            [Option( 's', "structure-dir" )]
            public bool DirectoryStructure { get; set; } = false;
        }

        public int Execute( ICommandOption opt )
        {
            var option = (CommandOption)opt;

            using var repository = new LiteDbKeySwitchRepository( option.DatabasePath );
            var translator = new KeySwitchToCakewalkArticulationModel();
            var presenter = new IExportingCakewalkArticulationPresenter.Null();
            var interactor = new ExportingCakewalkArticulationInteractor( repository, translator, presenter );

            var input = new ExportingCakewalkArticulationRequest( option.Developer, option.Product, option.Instrument );

            Console.WriteLine( $"Developer=\"{option.Developer}\", Product=\"{option.Product}\", Instrument=\"{option.Instrument}\"" );

            var response = interactor.Execute( input );

            if( response.Empty )
            {
                Console.WriteLine( "records not found" );
                return 0;
            }

            foreach( var i in response.Elements )
            {
                var outputDirectory = option.OutputDirectory;

                if( option.DirectoryStructure )
                {
                    outputDirectory = EntityDirectoryHelper.CreateDirectoryTree(
                        i.KeySwitch,
                        new DirectoryPath( option.OutputDirectory )
                    ).Path;
                }
                else
                {
                    DirectoryHelper.Create( outputDirectory );
                }

                var prefix = $"{i.KeySwitch.ProductName} {i.KeySwitch.InstrumentName}";
                var path = $"{prefix}.artmap";
                path = PathHelper.IncrementPathNameWhenExist( outputDirectory, path );

                Console.Out.WriteLine( $"export to {path}" );
                File.WriteAllText( path, i.JsonText.Value, Encoding.UTF8 );
            }

            return 0;
        }
    }
}