using System.IO;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper;
using KeySwitchManager.Infrastructures.Storage.Xml.Cubase.Helpers;

namespace KeySwitchManager.Infrastructures.Storage.Xml.Cubase
{
    public class CubaseFileRepository : SaveOnlyKeySwitchFileRepository
    {
        protected DirectoryPath TargetDirectory { get; }

        public CubaseFileRepository( DirectoryPath targetDirectory ) :
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
                    CreatePathHelper.CreateFilePath( x, ".expressionmap", TargetDirectory ).Path
                );

                TargetDirectory.CreateNew();

                using var stream = File.Create( outputFilePath.Path );

                CubaseFileWriter.Write( stream, x, LoggingSubject );
                saved++;
            }

            return saved;
        }
        #endregion

    }
}
