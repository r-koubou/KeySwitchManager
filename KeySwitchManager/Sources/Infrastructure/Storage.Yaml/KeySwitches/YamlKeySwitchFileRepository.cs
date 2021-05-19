using System;
using System.IO;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructure.Storage.KeySwitches;
using KeySwitchManager.Storage.Yaml.KeySwitches.Helpers;

namespace KeySwitchManager.Storage.Yaml.KeySwitches
{
    public class YamlKeySwitchFileRepository : KeySwitchFileRepository
    {
        public YamlKeySwitchFileRepository( IPath yamlDataPath, bool loadFromPathNow ) :
            base( yamlDataPath, loadFromPathNow )
        {
            if( !yamlDataPath.IsFile )
            {
                throw new ArgumentException( $"{nameof( yamlDataPath )} is not FilePath" );
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