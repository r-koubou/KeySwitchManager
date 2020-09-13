using System;
using System.Collections.Generic;
using System.IO;

using ArticulationManager.Common.Utilities;
using ArticulationManager.Databases.LiteDB.Articulations.Model;
using ArticulationManager.Databases.LiteDB.Translations;
using ArticulationManager.Domain.Articulations.Aggregate;
using ArticulationManager.Domain.Articulations.Value;
using ArticulationManager.Gateways.Articulations;

using LiteDB;

namespace ArticulationManager.Databases.LiteDB.Articulations
{
    public class LiteDbArticulationRepository : IArticulationRepository, IDisposable
    {
        public const string ArticulationsTableName = @"articulations";

        private LiteDatabase Database { get; set; }

        private ILiteCollection<ArticulationModel> ArticulationTable
            => Database.GetCollection<ArticulationModel>( ArticulationsTableName );

        public LiteDbArticulationRepository( string dbFilePath )
        {
            Database = new LiteDatabase( dbFilePath );
        }

        public LiteDbArticulationRepository( Stream stream )
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
            var table = Database.GetCollection<ArticulationModel>( ArticulationsTableName );
            return table.Count();
        }

        #region Save
        public void Save( Articulation articulation )
        {
            var table = Database.GetCollection<ArticulationModel>( ArticulationsTableName );

            var translator = new EntityToDbModel();
            var entity = translator.Translate( articulation );

            if( table.Exists( x => x.Id.Equals( articulation.Id.Value ) ) )
            {
                entity.LastUpdated = DateTimeHelper.NowUtc();
            }
            else
            {
                entity.Id = articulation.Id.Value;
            }

            table.Upsert( entity );
        }
        #endregion

        #region Delete
        public void Delete( Articulation articulation )
        {
            ArticulationTable.Delete( articulation.Id.Value );
        }

        public void Delete( DeveloperName developerName, ProductName productName )
        {
            ArticulationTable.DeleteMany(
                x =>
                    x.DeveloperName == developerName.Value &&
                    x.ProductName == productName.Value );
        }

        public void DeleteAll()
        {
            var table = Database.GetCollection<ArticulationModel>( ArticulationsTableName );
            table.DeleteAll();
        }
        #endregion

        #region Find
        private List<Articulation> CreateEntities( IEnumerable<ArticulationModel> query )
        {
            var result = new List<Articulation>();
            var translator = new DbModelToEntity();

            foreach( var item in query )
            {
                result.Add( translator.Translate( item ) );
            }

            return result;
        }

        public IEnumerable<Articulation> Find( DeveloperName developerName, ProductName productName )
        {
            return CreateEntities(
                ArticulationTable.Find(
                    x =>
                        x.DeveloperName == developerName.Value &&
                        x.ProductName   == productName.Value
                )
            );
        }

        public IEnumerable<Articulation> Find( DeveloperName developerName )
        {
            return CreateEntities( ArticulationTable.Find( x => x.DeveloperName == developerName.Value ) );
        }

        public IEnumerable<Articulation> Find( ProductName productName )
        {
            return CreateEntities( ArticulationTable.Find( x => x.ProductName == productName.Value ) );
        }

        public IEnumerable<Articulation> Find( ArticulationName articulationName )
        {
            return CreateEntities( ArticulationTable.Find( x => x.ArticulationName == articulationName.Value ) );
        }

        public IEnumerable<Articulation> FindAll()
        {
            return CreateEntities( ArticulationTable.FindAll() );
        }
        #endregion
    }
}