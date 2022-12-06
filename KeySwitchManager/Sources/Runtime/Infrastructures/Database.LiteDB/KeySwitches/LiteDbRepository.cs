using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Helpers;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Infrastructures.Database.LiteDB.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Database.LiteDB.KeySwitches.Translators;

using LiteDB;

using RkHelper.System;
using RkHelper.Time;

namespace KeySwitchManager.Infrastructures.Database.LiteDB.KeySwitches
{
    [Obsolete( "Use YamlhRepository instead" )]
    public class LiteDbRepository : IKeySwitchRepository
    {
        public const string KeySwitchesTableName = @"keyswitches";

        private readonly Subject<string> logging = new();
        public IObservable<string> OnLogging => logging;

        private LiteDatabase Database { get; set; }

        private ILiteCollection<KeySwitchModel> KeySwitchTable
            => Database.GetCollection<KeySwitchModel>( KeySwitchesTableName );

        public LiteDbRepository( FilePath dbFilePath )
        {
            Database = new LiteDatabase( dbFilePath.Path );
        }

        public void Dispose()
        {
            Disposer.Dispose( logging );
            Close();
        }

        private void Close()
        {
            lock( this )
            {
                if( Database == default! )
                {
                    return;
                }

                Database.Dispose();
                Database = default!;
            }
        }

        public int Count()
        {
            var table = Database.GetCollection<ArticulationModel>( KeySwitchesTableName );
            return table.Count();
        }

        #region Save
        public IKeySwitchRepository.SaveResult Save( KeySwitch keySwitch )
        {
            logging.OnNext( keySwitch.ToString() );

            var table = Database.GetCollection<KeySwitchModel>( KeySwitchesTableName );

            var translator = new KeySwitchExportTranslator();
            var entity = translator.Translate( keySwitch );
            var exist = table.Find( x => x.Id.Equals( keySwitch.Id.Value ), 0, 1 ).ToList();

            if( exist.Any() )
            {
                entity.Created     = DateTimeHelper.ToUtc( exist[ 0 ].Created );
                entity.LastUpdated = DateTimeHelper.NowUtc();
                var updated = table.Update( entity ) ? 1 : 0;

                return new IKeySwitchRepository.SaveResult(
                    inserted: 0,
                    updated: updated
                );
            }

            entity.Id = keySwitch.Id.Value;

            var insertedId = table.Insert( entity ).AsGuid;
            return new IKeySwitchRepository.SaveResult(
                inserted: insertedId == entity.Id ? 1 : 0,
                updated: 0
            );
        }

        #endregion

        #region Delete
        public int Delete( KeySwitchId keySwitchId )
        {
            var result = KeySwitchTable.Delete( keySwitchId.Value );
            return result ? 1 : 0;
        }

        public int Delete(
            DeveloperName developerName,
            ProductName productName,
            InstrumentName instrumentName )
        {
            var d = developerName.Value;
            var p = productName.Value;
            var i = instrumentName.Value;

            return KeySwitchTable.DeleteMany(
                x =>
                    ( d == DeveloperName.Any.Value || x.DeveloperName.Contains( d ) ) &&
                    ( p == ProductName.Any.Value || x.ProductName.Contains( p ) ) &&
                    ( i == InstrumentName.Any.Value || x.InstrumentName.Contains( i ) )
            );
        }

        public int Delete( DeveloperName developerName, ProductName productName )
        {
            var d = developerName.Value;
            var p = productName.Value;

            return KeySwitchTable.DeleteMany(
                x =>
                    ( d == DeveloperName.Any.Value || x.DeveloperName.Contains( d ) ) &&
                    ( p == ProductName.Any.Value || x.ProductName.Contains( p ) )
            );
        }

        public int Delete( DeveloperName developerName )
        {
            var d = developerName.Value;

            return KeySwitchTable.DeleteMany(
                x =>
                    d == DeveloperName.Any.Value || x.DeveloperName.Contains( d )
            );
        }

        public int Delete( ProductName productName )
        {
            var p = productName.Value;

            return KeySwitchTable.DeleteMany(
                x =>
                    p == ProductName.Any.Value || x.ProductName.Contains( p )
            );
        }

        public int Delete( InstrumentName instrumentName )
        {
            var i = instrumentName.Value;

            return KeySwitchTable.DeleteMany(
                x =>
                    i == InstrumentName.Any.Value || x.InstrumentName.Contains( i )
            );
        }

        public int DeleteAll()
        {
            var table = Database.GetCollection<ArticulationModel>( KeySwitchesTableName );
            return table.DeleteAll();
        }
        #endregion

        #region Find
        private IReadOnlyCollection<KeySwitch> CreateEntities( IEnumerable<KeySwitchModel> query )
        {
            var result = new List<KeySwitch>();
            var translator = new KeySwitchImportTranslator();

            foreach( var item in query )
            {
                result.Add( translator.Translate( item ) );
            }

            return KeySwitchHelper.SortByAlphabetical( result );
        }

        public IReadOnlyCollection<KeySwitch> Find( KeySwitchId keySwitchId )
        {
            return CreateEntities( KeySwitchTable.Find( x => x.Id == keySwitchId.Value ) );
        }

        public IReadOnlyCollection<KeySwitch> Find(
            DeveloperName developerName,
            ProductName productName,
            InstrumentName instrumentName )
        {
            var d = developerName.Value;
            var p = productName.Value;
            var i = instrumentName.Value;

            return CreateEntities(
                KeySwitchTable.Find(
                    x =>
                        ( d == DeveloperName.Any.Value  || x.DeveloperName.Contains( d ) ) &&
                        ( p == ProductName.Any.Value    || x.ProductName.Contains( p ) )   &&
                        ( i == InstrumentName.Any.Value || x.InstrumentName.Contains( i ) )
                )
            );
        }

        public IReadOnlyCollection<KeySwitch> Find( DeveloperName developerName, ProductName productName )
        {
            var d = developerName.Value;
            var p = productName.Value;

            return CreateEntities(
                KeySwitchTable.Find(
                    x =>
                        ( d == DeveloperName.Any.Value || x.DeveloperName.Contains( d ) ) &&
                        ( p == ProductName.Any.Value   || x.ProductName.Contains( p ) )
                )
            );
        }

        public IReadOnlyCollection<KeySwitch> Find( DeveloperName developerName )
        {
            var d = developerName.Value;

            if( d == DeveloperName.Any.Value )
            {
                return FindAll();
            }

            return CreateEntities(
                KeySwitchTable.Find(
                    x =>
                        x.DeveloperName.Contains( d )
                )
            );
        }

        public IReadOnlyCollection<KeySwitch> Find( ProductName productName )
        {
            var p = productName.Value;

            if( p == ProductName.Any.Value )
            {
                return FindAll();
            }

            return CreateEntities(
                KeySwitchTable.Find(
                    x =>
                        x.ProductName.Contains( p )
                )
            );
        }

        public IReadOnlyCollection<KeySwitch> Find( InstrumentName instrumentName )
        {
            var i = instrumentName.Value;

            return CreateEntities(
                KeySwitchTable.Find(
                    x =>
                        i == InstrumentName.Any.Value || x.InstrumentName.Contains( i )
                )
            );
        }

        public IReadOnlyCollection<KeySwitch> FindAll()
        {
            return CreateEntities( KeySwitchTable.FindAll() );
        }
        #endregion
    }
}