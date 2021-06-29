using System;
using System.IO;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructure.Storage.KeySwitches;
using KeySwitchManager.Storage.Yaml.KeySwitches.Helpers;

namespace KeySwitchManager.Storage.Yaml.KeySwitches
{
    public class YamlKeySwitchFileRepository : KeySwitchFileRepository
    {
        private IStorageAccessListener StorageAccessListener { get; }

        public YamlKeySwitchFileRepository( IPath yamlDataPath, bool loadFromPathNow ) :
            this( yamlDataPath, loadFromPathNow, IStorageAccessListener.Null )
        {}

        public YamlKeySwitchFileRepository( IPath yamlDataPath, bool loadFromPathNow, IStorageAccessListener listener ) :
            base( yamlDataPath, loadFromPathNow )
        {
            if( !yamlDataPath.IsFile )
            {
                throw new ArgumentException( $"{nameof( yamlDataPath )} is not FilePath" );
            }

            StorageAccessListener = listener;
        }

        #region Write to file
        public override int Flush()
        {
            var saved = KeySwitches.Count;

            StorageAccessListener.OnWriteAccess( KeySwitches, DataPath );

            using var stream = File.Create( DataPath.Path );
            KeySwitchFileWriter.Write( stream, KeySwitches );

            return saved;
        }
        #endregion

        #region Load from file
        public override void Load()
        {
            StorageAccessListener.OnReadAccess( DataPath );

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