using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using ArticulationManager.Common.Utilities;
using ArticulationManager.Databases.LiteDB.Articulations.Model;
using ArticulationManager.Databases.LiteDB.Articulations.Service;
using ArticulationManager.Domain.Articulations.Aggregate;
using ArticulationManager.Domain.Articulations.Value;
using ArticulationManager.Gateways.Articulations;

using LiteDB;

namespace ArticulationManager.Databases.LiteDB.Articulations
{
    public class LiteDatabaseRepository : IArticulationRepository, IDisposable
    {
        private const string ArticulationsTableName = @"articulations";

        private LiteDatabase Database { get; set; } = default!;

        public LiteDatabaseRepository( string dbFilePath )
        {
            Open( dbFilePath );
        }

        public void Dispose()
        {
            Close();
        }

        private void Open( string dbFilePath )
        {
            Close();
            Database = new LiteDatabase( dbFilePath );
        }

        public void Close()
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

        public void Save( Articulation articulation )
        {
            using var db = Database;
            var table = db.GetCollection<ArticulationModel>( ArticulationsTableName );

            var translator = new ArticulationTranslationService();
            var entity = translator.Translate( articulation );

            if( table.Exists( x => x.Id.Equals( Guid.Parse( articulation.Id.Value ) ) ) )
            {
                entity.LastUpdated = DateTimeHelper.NowUtc();
            }
            else
            {
                entity.Id = Guid.Parse( articulation.Id.Value );
            }

            table.Upsert( entity );
        }

        public void Remove( Articulation articulation )
        {
            using var db = Database;
            var table = db.GetCollection<ArticulationModel>( ArticulationsTableName );

            table.Delete( new ObjectId( articulation.Id.Value ) );

        }

        private List<Articulation> CreateEntities( IEnumerable<ArticulationModel> query )
        {
            var result = new List<Articulation>();
            var translator = new ArticulationModelTranslationService();

            foreach( var item in query )
            {
                result.Add( translator.Translate( item ) );
            }

            return result;
        }

        public IEnumerable<T> Find<T>( Expression<Func<T, bool>> predicate, int skip = 0, int limit = 2147483647 )
        {
            var table = Database.GetCollection<T>( ArticulationsTableName );
            return table.Find( predicate, skip, limit );
        }

        public IEnumerable<Articulation> Find( DeveloperName developerName )
        {
            return CreateEntities( Find<ArticulationModel>( x => developerName.Equals( new DeveloperName( x.DeveloperName ) ) ) );
        }

        public IEnumerable<Articulation> Find( ProductName productName )
        {
            return CreateEntities( Find<ArticulationModel>( x => productName.Equals( new ProductName( x.ProductName ) ) ) );
        }
    }
}