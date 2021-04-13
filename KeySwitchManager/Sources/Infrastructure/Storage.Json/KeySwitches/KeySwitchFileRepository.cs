using System;
using System.IO;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructure.Storage.Json.KeySwitches.Helpers;
using KeySwitchManager.Infrastructure.Storage.KeySwitches;

namespace KeySwitchManager.Infrastructure.Storage.Json.KeySwitches
{
    public class KeySwitchFileRepository : FileRepository
    {
        public KeySwitchFileRepository( IPath jsonDataPath, bool loadFromPathNow ) :
            base( jsonDataPath, loadFromPathNow )
        {
            if( !jsonDataPath.IsFile )
            {
                throw new ArgumentException( $"{nameof( jsonDataPath )} is not FilePath" );
            }
        }

        #region Write to file
        public override int Flush()
        {
            var saved = KeySwitches.Count;
            using var stream = File.Create( DataPath.Path );
            KeySwitchFileWriter.Write( stream, KeySwitches );

            return saved;
        }
        #endregion

        #region Load from file
        public override void Load()
        {
            if( !DataPath.Exists )
            {
                throw new FileNotFoundException( DataPath.Path );
            }

            using var stream = File.Open( DataPath.Path, FileMode.Open );
            KeySwitches.Clear();
            KeySwitches.AddRange(  KeySwitchFileReader.Read( stream ) );
        }
        #endregion

    }
}