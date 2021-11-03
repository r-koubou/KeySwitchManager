using System;
using System.IO;
using System.Text;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;

namespace KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches
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
        [Obsolete]
        public override int Flush()
        {
            // TODO メソッドを消す

            throw new NotSupportedException();
#if false
            var saved = KeySwitches.Count;

            if( LoggingSubject.HasObservers )
            {
                foreach( var k in KeySwitches )
                {
                    LoggingSubject.OnNext( k.ToString() );
                }
            }

            using var stream = File.Create( DataPath.Path );
            using var writer = new YamlKeySwitchWriter( stream );

            writer.Write( KeySwitches, LoggingSubject );

            return saved;
#endif
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
            using var reader = new KeySwitchFileReader( stream );
            KeySwitches.Clear();
            KeySwitches.AddRange( reader.Read( LoggingSubject ) );
        }
        #endregion

    }
}