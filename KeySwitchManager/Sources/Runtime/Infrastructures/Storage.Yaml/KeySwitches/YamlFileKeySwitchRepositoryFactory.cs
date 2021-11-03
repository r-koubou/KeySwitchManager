using System.IO;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;

namespace KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches
{
    public class YamlFileKeySwitchRepositoryFactory : KeySwitchFileRepositoryFactory
    {
        public YamlFileKeySwitchRepositoryFactory( FilePath dataPath ) : base( dataPath ) {}

        public override IKeySwitchRepository Create()
        {
            if( !DataPath.Exists )
            {
                throw new FileNotFoundException( DataPath.Path );
            }

            using var stream = File.Open( DataPath.Path, FileMode.Open );
            using var reader = new KeySwitchFileReader( stream );

            return new OnMemoryKeySwitchRepository( reader.Read() );
        }
    }
}
