using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Helpers;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Models;
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

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public int Count()
            => YamlModel.KeySwitches.Count;

        #region Save
        public IKeySwitchRepository.SaveResult Save( KeySwitch keySwitch )
        {
            var yamlModels = YamlModel.KeySwitches;
            var model = TranslateModelHelper.Translate( keySwitch );
            var exist = yamlModels.Find( x => x.Id.Equals( keySwitch.Id.Value ) );

            model.LastUpdated = model.Created;

            if( exist != null )
            {
                var index = yamlModels.IndexOf( exist );

                model.LastUpdated   = UtcDateTime.NowAsDateTime;
                yamlModels[ index ] = model;
                return new IKeySwitchRepository.SaveResult( 0, 1 );
            }

            yamlModels.Add( model );
            return new IKeySwitchRepository.SaveResult( 1, 0 );
        }

        public int Flush()
        {
            using var stream = YamlFilePath.OpenWriteStream();
            using var writer = new StreamWriter( stream, Encoding.UTF8 );
            var serializer = new Serializer();
            serializer.Serialize( writer, YamlModel );

            return Count();
        }

        #endregion

        #region Delete
        public int Delete( DeveloperName developerName, ProductName productName, InstrumentName instrumentName )
        {
            var keySwitches = YamlModel.KeySwitches;
            var founds = keySwitches.Where( x =>
                                                 x.DeveloperName == developerName.Value &&
                                                 x.ProductName == productName.Value &&
                                                 x.InstrumentName == instrumentName.Value
            );

            return founds.Sum( x => keySwitches.Remove( x ) ? 1 : 0 );
        }

        public int Delete( DeveloperName developerName, ProductName productName )
        {
            var keySwitches = YamlModel.KeySwitches;
            var founds = keySwitches.Where( x =>
                                                x.DeveloperName == developerName.Value &&
                                                x.ProductName == productName.Value
            );

            return founds.Sum( x => keySwitches.Remove( x ) ? 1 : 0 );
        }

        public int Delete( DeveloperName developerName )
        {
            var keySwitches = YamlModel.KeySwitches;
            var founds = keySwitches.Where( x => x.DeveloperName == developerName.Value );

            return founds.Sum( x => keySwitches.Remove( x ) ? 1 : 0 );
        }

        public int Delete( ProductName productName )
        {
            var keySwitches = YamlModel.KeySwitches;
            var founds = keySwitches.Where( x => x.ProductName == productName.Value );

            return founds.Sum( x => keySwitches.Remove( x ) ? 1 : 0 );

        }

        public int Delete( InstrumentName instrumentName )
        {
            var keySwitches = YamlModel.KeySwitches;
            var founds = keySwitches.Where( x => x.InstrumentName == instrumentName.Value );

            return founds.Sum( x => keySwitches.Remove( x ) ? 1 : 0 );
        }

        public int Delete( KeySwitchId keySwitchId )
        {
            var keySwitches = YamlModel.KeySwitches;
            var founds = keySwitches.Where( x => x.Id == keySwitchId.Value );

            return founds.Sum( x => keySwitches.Remove( x ) ? 1 : 0 );
        }

        public int DeleteAll()
        {
            var count = YamlModel.KeySwitches.Count;
            YamlModel.KeySwitches.Clear();
            return count;

        }
        #endregion

        #region Find
        public IReadOnlyCollection<KeySwitch> Find( KeySwitchId keySwitchId )
        {
            var result = new List<KeySwitch>();
            var founds = YamlModel.KeySwitches.Where( x => x.Id == keySwitchId.Value );

            foreach( var x in founds )
            {
                result.Add( TranslateKeySwitchHelper.Translate( x ) );
            }

            return KeySwitchHelper.SortByAlphabetical( result );
        }

        public IReadOnlyCollection<KeySwitch> Find( DeveloperName developerName, ProductName productName, InstrumentName instrumentName )
        {
            var result = new List<KeySwitch>();
            var founds =
                YamlModel.KeySwitches.FindAll(
                    x =>
                        x.DeveloperName == developerName.Value &&
                        x.ProductName == productName.Value &&
                        x.InstrumentName == instrumentName.Value
            );

            foreach( var x in founds )
            {
                result.Add( TranslateKeySwitchHelper.Translate( x ) );
            }

            return KeySwitchHelper.SortByAlphabetical( result );
        }

        public IReadOnlyCollection<KeySwitch> Find( DeveloperName developerName, ProductName productName )
        {
            var result = new List<KeySwitch>();
            var founds =
                YamlModel.KeySwitches.FindAll(
                    x =>
                        x.DeveloperName == developerName.Value &&
                        x.ProductName == productName.Value
                );

            foreach( var x in founds )
            {
                result.Add( TranslateKeySwitchHelper.Translate( x ) );
            }

            return KeySwitchHelper.SortByAlphabetical( result );
        }

        public IReadOnlyCollection<KeySwitch> Find( DeveloperName developerName )
        {
            var result = new List<KeySwitch>();
            var founds =
                YamlModel.KeySwitches.FindAll( x => x.DeveloperName == developerName.Value );

            foreach( var x in founds )
            {
                result.Add( TranslateKeySwitchHelper.Translate( x ) );
            }

            return KeySwitchHelper.SortByAlphabetical( result );
        }

        public IReadOnlyCollection<KeySwitch> Find( ProductName productName )
        {
            var result = new List<KeySwitch>();
            var founds =
                YamlModel.KeySwitches.FindAll( x => x.ProductName == productName.Value );

            foreach( var x in founds )
            {
                result.Add( TranslateKeySwitchHelper.Translate( x ) );
            }

            return KeySwitchHelper.SortByAlphabetical( result );
        }

        public IReadOnlyCollection<KeySwitch> Find( InstrumentName instrumentName )
        {
            var result = new List<KeySwitch>();
            var founds =
                YamlModel.KeySwitches.FindAll( x => x.InstrumentName == instrumentName.Value );

            foreach( var x in founds )
            {
                result.Add( TranslateKeySwitchHelper.Translate( x ) );
            }

            return KeySwitchHelper.SortByAlphabetical( result );
        }

        public IReadOnlyCollection<KeySwitch> FindAll()
        {
            var result = new List<KeySwitch>();
            foreach( var x in YamlModel.KeySwitches )
            {
                result.Add( TranslateKeySwitchHelper.Translate( x ) );
            }

            return KeySwitchHelper.SortByAlphabetical( result );
        }
        #endregion
    }
}
