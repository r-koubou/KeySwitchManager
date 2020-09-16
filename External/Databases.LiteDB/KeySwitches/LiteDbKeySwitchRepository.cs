using System;
using System.Collections.Generic;
using System.IO;

using KeySwitchManager.Common.Utilities;
using KeySwitchManager.Databases.LiteDB.KeySwitches.Model;
using KeySwitchManager.Databases.LiteDB.KeySwitches.Translations;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Gateways.KeySwitches;

using LiteDB;

namespace KeySwitchManager.Databases.LiteDB.KeySwitches
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
        public void Save( KeySwitch keySwitch )
        {
            var table = Database.GetCollection<KeySwitchModel>( KeySwitchesTableName );

            var translator = new EntityToDbModel();
            var entity = translator.Translate( keySwitch );

            if( table.Exists( x => x.Id.Equals( keySwitch.Id.Value ) ) )
            {
                entity.LastUpdated = DateTimeHelper.NowUtc();
            }
            else
            {
                entity.Id = keySwitch.Id.Value;
            }

            table.Upsert( entity );
        }
        #endregion

        #region Delete
        public void Delete( KeySwitch keySwitch )
        {
            KeySwitchTable.Delete( keySwitch.Id.Value );
        }

        public void Delete(
            DeveloperName developerName,
            ProductName productName,
            InstrumentName instrumentName )
        {
            KeySwitchTable.DeleteMany(
                x =>
                    x.DeveloperName == developerName.Value &&
                    x.ProductName == productName.Value &&
                    x.InstrumentName == instrumentName.Value );
        }

        public void Delete( DeveloperName developerName, ProductName productName )
        {
            KeySwitchTable.DeleteMany(
                x =>
                    x.DeveloperName == developerName.Value &&
                    x.ProductName == productName.Value );
        }

        public void DeleteAll()
        {
            var table = Database.GetCollection<ArticulationModel>( KeySwitchesTableName );
            table.DeleteAll();
        }
        #endregion

        #region Find
        private List<KeySwitch> CreateEntities( IEnumerable<KeySwitchModel> query )
        {
            var result = new List<KeySwitch>();
            var translator = new DbModelToEntity();

            foreach( var item in query )
            {
                result.Add( translator.Translate( item ) );
            }

            return result;
        }

        public IEnumerable<KeySwitch> Find(
            DeveloperName developerName,
            ProductName productName,
            InstrumentName instrumentName )
        {
            return CreateEntities(
                KeySwitchTable.Find(
                    x =>
                        x.DeveloperName == developerName.Value &&
                        x.ProductName == productName.Value &&
                        x.InstrumentName == instrumentName.Value
                )
            );
        }

        public IEnumerable<KeySwitch> Find( DeveloperName developerName, ProductName productName )
        {
            return CreateEntities(
                KeySwitchTable.Find(
                    x =>
                        x.DeveloperName == developerName.Value &&
                        x.ProductName == productName.Value
                )
            );
        }

        public IEnumerable<KeySwitch> Find( DeveloperName developerName )
        {
            return CreateEntities( KeySwitchTable.Find( x => x.DeveloperName == developerName.Value ) );
        }

        public IEnumerable<KeySwitch> Find( ProductName productName )
        {
            return CreateEntities( KeySwitchTable.Find( x => x.ProductName == productName.Value ) );
        }

        public IEnumerable<KeySwitch> FindAll()
        {
            return CreateEntities( KeySwitchTable.FindAll() );
        }
        #endregion
    }
}