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

            using var stream = DataPath.OpenReadStream();
            using var reader = new YamlKeySwitchReader( stream );

            return new OnMemoryKeySwitchRepository( reader.Read() );
        }
    }
}
