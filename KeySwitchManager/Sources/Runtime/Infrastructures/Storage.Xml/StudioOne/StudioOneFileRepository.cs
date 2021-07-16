using System.IO;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper;
using KeySwitchManager.Infrastructures.Storage.Xml.StudioOne.Helpers;

namespace KeySwitchManager.Infrastructures.Storage.Xml.StudioOne
{
    public class StudioOneFileRepository : SaveOnlyKeySwitchFileRepository
    {
        protected DirectoryPath TargetDirectory { get; }

        public StudioOneFileRepository( DirectoryPath targetDirectory ) :
            base( targetDirectory )
        {
            TargetDirectory = targetDirectory;
        }

        #region Write to file
        public override int Flush()
        {
            var saved = 0;

            foreach( var x in KeySwitches )
            {
                var outputFilePath = new FilePath(
                    CreatePathHelper.CreateFilePath( x, $"{x.ProductName.Value} - ", ".keyswitch", TargetDirectory ).Path
                );

                using var stream = File.Create( outputFilePath.Path );
                StudioOneFileWriter.Write( stream, x, LoggingSubject );

                saved++;
            }

            return saved;
        }
        #endregion

    }
}
