using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Database.LiteDB.KeySwitch.KeySwitch.Models;
using Database.LiteDB.KeySwitch.KeySwitch.Translations;

using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Gateways.KeySwitch;
using KeySwitchManager.Gateways.KeySwitch.Value;

using LiteDB;

using RkHelper.Time;

namespace Database.LiteDB.KeySwitch.KeySwitch
{
    public class LiteDbKeySwitchRepository : IKeySwitchRepository, IDisposable
    {
        public const string KeySwitchesTableName = @"keyswitches";

        private LiteDatabase Database { get; set; }

        private ILiteCollection<KeySwitchModel> KeySwitchTable
            => Database.GetCollection<KeySwitchModel>( KeySwitchesTableName );

        public LiteDbKeySwitchRepository( string dbFilePath )
        {
            Database = new LiteDatabase( dbFilePath );
        }

        public LiteDbKeySwitchRepository( Stream stream )
        {
            Database = new LiteDatabase( stream );
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
        public SaveResult Save( KeySwitchManager.Domain.KeySwitches.KeySwitch keySwitch )
        {
            var table = Database.GetCollection<KeySwitchModel>( KeySwitchesTableName );

            var translator = new EntityToDbModel();
            var entity = translator.Translate( keySwitch );
            var exist = table.Find( x => x.Id.Equals( keySwitch.Id.Value ), 0, 1 ).ToList();

            if( exist.Any() )
            {
                entity.Created     = DateTimeHelper.ToUtc( exist[ 0 ].Created );
                entity.LastUpdated = DateTimeHelper.NowUtc();
                var updated = table.Update( entity ) ? 1 : 0;

                return new SaveResult(
                    inserted: 0,
                    updated: updated
                );
            }

            entity.Id = keySwitch.Id.Value;

            var insertedId = table.Insert( entity ).AsGuid;
            return new SaveResult(
                inserted: insertedId == entity.Id ? 1 : 0,
                updated: 0
            );
        }
        #endregion

        #region Delete
        public int Delete( KeySwitchId guid )
        {
            var result = KeySwitchTable.Delete( guid.Value );
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
        private IReadOnlyCollection<KeySwitchManager.Domain.KeySwitches.KeySwitch> CreateEntities( IEnumerable<KeySwitchModel> query )
        {
            var result = new List<KeySwitchManager.Domain.KeySwitches.KeySwitch>();
            var translator = new DbModelToEntity();

            foreach( var item in query )
            {
                result.Add( translator.Translate( item ) );
            }

            return result;
        }

        public IReadOnlyCollection<KeySwitchManager.Domain.KeySwitches.KeySwitch> Find( KeySwitchId keySwitchId )
        {
            return CreateEntities( KeySwitchTable.Find( x => x.Id == keySwitchId.Value ) );
        }

        public IReadOnlyCollection<KeySwitchManager.Domain.KeySwitches.KeySwitch> Find(
            DeveloperName developerName,
            ProductName productName,
            InstrumentName instrumentName )
        {
            return CreateEntities(
                KeySwitchTable.Find(
                    x =>
                        x.DeveloperName.Contains( developerName.Value ) &&
                        x.ProductName.Contains( productName.Value ) &&
                        x.InstrumentName.Contains( instrumentName.Value )
                )
            );
        }

        public IReadOnlyCollection<KeySwitchManager.Domain.KeySwitches.KeySwitch> Find( DeveloperName developerName, ProductName productName )
        {
            return CreateEntities(
                KeySwitchTable.Find(
                    x =>
                        x.DeveloperName.Contains( developerName.Value ) &&
                        x.ProductName.Contains( productName.Value )
                )
            );
        }

        public IReadOnlyCollection<KeySwitchManager.Domain.KeySwitches.KeySwitch> Find( DeveloperName developerName )
        {
            return CreateEntities(
                KeySwitchTable.Find(
                    x =>
                        x.DeveloperName.Contains( developerName.Value )
                )
            );
        }

        public IReadOnlyCollection<KeySwitchManager.Domain.KeySwitches.KeySwitch> Find( ProductName productName )
        {
            return CreateEntities(
                KeySwitchTable.Find(
                    x =>
                        x.ProductName.Contains( productName.Value )
                )
            );
        }

        public IReadOnlyCollection<KeySwitchManager.Domain.KeySwitches.KeySwitch> FindAll()
        {
            return CreateEntities( KeySwitchTable.FindAll() );
        }
        #endregion
    }
}