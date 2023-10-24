using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Helpers;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Models.Aggregations;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Models.Extensions;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Translators.Helpers;

using YamlDotNet.Serialization;

namespace KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches
{
    public sealed class YamlRepository : IKeySwitchRepository
    {
        private FilePath YamlFilePath { get; }
        private YamlModel YamlModel { get; }

        private readonly Subject<string> logging = new();

        public IObservable<string> OnLogging => logging;

        public YamlRepository(FilePath filePath)
        {
            YamlFilePath = filePath;

            var yaml = File.ReadAllText( YamlFilePath.Path, Encoding.UTF8 );
            var deserializer = new DeserializerBuilder().Build();

            YamlModel = deserializer.Deserialize<YamlModel>( yaml );
        }

        public async void Dispose()
        {
            await FlushAsync();
        }

        public int Count()
            => YamlModel.KeySwitches.Count;

        #region Save
        public async Task<IKeySwitchRepository.SaveResult> SaveAsync( KeySwitch keySwitch )
        {
            var yamlModels = YamlModel.KeySwitches;
            var model = TranslateModelHelper.Translate( keySwitch );
            var exist = yamlModels.Find( x => x.Id.Equals( keySwitch.Id.Value ) );

            model.LastUpdated = model.Created;

            if( exist != null )
            {
                var index = yamlModels.IndexOf( exist );

                model.Created       = exist.Created;
                model.LastUpdated   = UtcDateTime.NowAsDateTime;
                yamlModels[ index ] = model;
                return await Task.FromResult( new IKeySwitchRepository.SaveResult( 0, 1 ) );
            }

            yamlModels.Add( model );
            return await Task.FromResult( new IKeySwitchRepository.SaveResult( 1, 0 ) );
        }

        public async Task<int> FlushAsync()
        {
            await using var stream = YamlFilePath.OpenWriteStream();
            await using var writer = new StreamWriter( stream, Encoding.UTF8 );
            var serializer = new Serializer();
            serializer.Serialize( writer, YamlModel );

            return Count();
        }

        #endregion

        #region Delete
        public async Task<int> DeleteAsync( DeveloperName developerName, ProductName productName, InstrumentName instrumentName )
        {
            var keySwitches = YamlModel.KeySwitches;
            var founds = YamlModel.Find( developerName, productName, instrumentName );

            return await Task.FromResult( founds.Sum( x => keySwitches.Remove( x ) ? 1 : 0 ) );
        }

        public async Task<int> DeleteAsync( DeveloperName developerName, ProductName productName )
        {
            var keySwitches = YamlModel.KeySwitches;
            var founds = YamlModel.Find( developerName, productName );

            return await Task.FromResult( founds.Sum( x => keySwitches.Remove( x ) ? 1 : 0 ) );
        }

        public async Task<int> DeleteAsync( DeveloperName developerName )
        {
            var keySwitches = YamlModel.KeySwitches;
            var founds = YamlModel.Find( developerName );

            return await Task.FromResult( founds.Sum( x => keySwitches.Remove( x ) ? 1 : 0 ) );
        }

        public async Task<int> DeleteAsync( ProductName productName )
        {
            var keySwitches = YamlModel.KeySwitches;
            var founds = YamlModel.Find( productName );

            return await Task.FromResult( founds.Sum( x => keySwitches.Remove( x ) ? 1 : 0 ) );

        }

        public async Task<int> DeleteAsync( InstrumentName instrumentName )
        {
            var keySwitches = YamlModel.KeySwitches;
            var founds = YamlModel.Find( instrumentName );

            return await Task.FromResult( founds.Sum( x => keySwitches.Remove( x ) ? 1 : 0 ) );
        }

        public async Task<int> DeleteAsync( KeySwitchId keySwitchId )
        {
            var keySwitches = YamlModel.KeySwitches;
            var founds = YamlModel.Find( keySwitchId );

            return await Task.FromResult( founds.Sum( x => keySwitches.Remove( x ) ? 1 : 0 ) );
        }

        public async Task<int> DeleteAllAsync()
        {
            var count = YamlModel.KeySwitches.Count;
            YamlModel.KeySwitches.Clear();
            return await Task.FromResult( count );

        }
        #endregion

        #region Find
        private static async Task<IReadOnlyCollection<KeySwitch>> ConvertAsync( IReadOnlyCollection<KeySwitchModel> source )
        {
            var result = new List<KeySwitch>();

            foreach( var x in source )
            {
                result.Add( TranslateKeySwitchHelper.Translate( x ) );
            }

            return await Task.FromResult( KeySwitchHelper.SortByAlphabetical( result ) );
        }


        public async Task<IReadOnlyCollection<KeySwitch>> FindAsync( KeySwitchId keySwitchId )
        {
            return await ConvertAsync( YamlModel.Find( keySwitchId ) );
        }

        public async Task<IReadOnlyCollection<KeySwitch>> FindAsync( DeveloperName developerName, ProductName productName, InstrumentName instrumentName )
        {
            return await ConvertAsync( YamlModel.Find( developerName, productName, instrumentName ) );
        }

        public async Task<IReadOnlyCollection<KeySwitch>> FindAsync( DeveloperName developerName, ProductName productName )
        {
            return await ConvertAsync( YamlModel.Find( developerName, productName ) );
        }

        public async Task<IReadOnlyCollection<KeySwitch>> FindAsync( DeveloperName developerName )
        {
            return await ConvertAsync( YamlModel.Find( developerName ) );
        }

        public async Task<IReadOnlyCollection<KeySwitch>> FindAsync( ProductName productName )
        {
            return await ConvertAsync( YamlModel.Find( productName ) );
        }

        public async Task<IReadOnlyCollection<KeySwitch>> FindAsync( InstrumentName instrumentName )
        {
            return await ConvertAsync( YamlModel.Find( instrumentName ) );
        }

        public async Task<IReadOnlyCollection<KeySwitch>> FindAllAsync()
        {
            return await ConvertAsync( YamlModel.KeySwitches );
        }
        #endregion
    }
}
