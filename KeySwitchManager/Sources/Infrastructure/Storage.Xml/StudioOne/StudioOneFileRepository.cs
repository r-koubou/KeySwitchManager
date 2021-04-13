using System.IO;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructure.Storage.KeySwitches;
using KeySwitchManager.Infrastructure.Storage.KeySwitches.Helper;
using KeySwitchManager.Infrastructure.Storage.Xml.StudioOne.Helpers;

namespace KeySwitchManager.Infrastructure.Storage.Xml.StudioOne
{
    public class StudioOneFileRepository : SaveOnlyFileRepository
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
                    CreatePathHelper.CreateFilePath( x, ".keyswitch", TargetDirectory ).Path
                );

                using var stream = File.Create( outputFilePath.Path );
                StudioOneFileWriter.Write( stream, x );

                saved++;
            }

            return saved;
        }
        #endregion

    }
}
