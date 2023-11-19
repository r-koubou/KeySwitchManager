using System;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches;

namespace KeySwitchManager.Applications.Standalone.Core.Helpers
{
    public static class KeySwitchRepositoryFactory
    {
        public static IKeySwitchRepository CreateFileRepository(string filePath)
        {
            var path = filePath.ToLower();

            if( path.EndsWith( ".yaml" ) || path.EndsWith( ".yml" ) )
            {
                return new YamlFileRepository( new FilePath( filePath ) );
            }

            throw new ArgumentException( $"{filePath} is unknown file format" );
        }
    }
}
