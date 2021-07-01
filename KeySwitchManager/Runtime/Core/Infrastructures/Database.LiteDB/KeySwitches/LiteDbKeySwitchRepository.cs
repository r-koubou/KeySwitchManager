using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Infrastructures.Database.LiteDB.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Database.LiteDB.KeySwitches.Translators;

using LiteDB;

using RkHelper.Time;

namespace KeySwitchManager.Infrastructures.Database.LiteDB.KeySwitches
{
    public class LiteDbKeySwitchRepository : IKeySwitchRepository
    {
        public const string KeySwitchesTableName = @"keyswitches";

        private Subject<string> LoggingSubject { get; }
        public IObservable<string> LoggingObservable { get; }

        private LiteDatabase Database { get; set; }

        private ILiteCollection<KeySwitchModel> KeySwitchTable
            => Database.GetCollection<KeySwitchModel>( KeySwitchesTableName );

        public LiteDbKeySwitchRepository( FilePath dbFilePath )
        {
            LoggingSubject    = new Subject<string>();
            LoggingObservable = LoggingSubject.AsObservable();
            Database          = new LiteDatabase( dbFilePath.Path );
        }

        public void Dispose()
        {
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
            LoggingSubject.OnNext( keySwitch.ToString() );

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

        public int Flush()
            => 1;
        // Always return 1 (A database file will be flushed to storage with disposing)
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
            return KeySwitchTable.DeleteMany(
                x =>
                    x.DeveloperName == developerName.Value &&
                    x.ProductName == productName.Value &&
                    x.InstrumentName == instrumentName.Value );
        }

        public int Delete( DeveloperName developerName, ProductName productName )
        {
            return KeySwitchTable.DeleteMany(
                x =>
                    x.DeveloperName == developerName.Value &&
                    x.ProductName == productName.Value );
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

            return result;
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
                        ( d == "*" || x.DeveloperName.Contains( d ) ) &&
                        ( p == "*" || x.ProductName.Contains( p ) ) &&
                        ( i == "*" || x.InstrumentName.Contains( i ) )
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
                        ( d == "*" || x.DeveloperName.Contains( d ) ) &&
                        ( p == "*" || x.ProductName.Contains( p ) )
                )
            );
        }

        public IReadOnlyCollection<KeySwitch> Find( DeveloperName developerName )
        {
            var d = developerName.Value;

            if( d == "*" )
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

            if( p == "*" )
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

        public IReadOnlyCollection<KeySwitch> FindAll()
        {
            return CreateEntities( KeySwitchTable.FindAll() );
        }
        #endregion
    }
}