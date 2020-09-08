using System;
using System.Collections.Generic;
using System.IO;
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
    public class LiteDbArticulationRepository : IArticulationRepository, IDisposable
    {
        public const string ArticulationsTableName = @"articulations";

        private LiteDatabase Database { get; set; }

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

        public void Save( Articulation articulation )
        {
            var table = Database.GetCollection<ArticulationModel>( ArticulationsTableName );

            var translator = new ArticulationTranslationService();
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

        public void DeleteMany<T>( Expression<Func<T, bool>> predicate )
        {
            var table = Database.GetCollection<T>( ArticulationsTableName );
            table.DeleteMany( predicate );
        }

        public void Delete( Articulation articulation )
        {
            var table = Database.GetCollection<ArticulationModel>( ArticulationsTableName );
            table.Delete( articulation.Id.Value );
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

        public IEnumerable<Articulation> FindAll()
        {
            var table = Database.GetCollection<ArticulationModel>( ArticulationsTableName );
            return CreateEntities( table.FindAll() );
        }

    }
}