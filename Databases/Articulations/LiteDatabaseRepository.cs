using System;
using System.Collections.Generic;

using ArticulationManager.Databases.Articulations.Model;
using ArticulationManager.Databases.Articulations.Service;
using ArticulationManager.Domain.Articulations.Aggregate;
using ArticulationManager.Domain.Articulations.Value;
using ArticulationManager.Gateways.Articulations;

using LiteDB;

namespace ArticulationManager.Databases.Articulations
{
    public class LiteDatabaseRepository : IArticulationRepository
    {
        private const string ArticulationsTableName = @"articulations";

        private string DbFilePath { get; }

        public LiteDatabaseRepository( string dbFilePath )
        {
            DbFilePath = dbFilePath;
        }

        private LiteDatabase OpenDatabase()
        {
            return new LiteDatabase( DbFilePath );
        }

        public void Save( Articulation articulation )
        {
            using var db = OpenDatabase();
            var table = db.GetCollection<ArticulationModel>( ArticulationsTableName );

            var translator = new ArticulationTranslationService();
            var entity = translator.Translate( articulation );

            if( table.Exists( x => x.Guid == articulation.Guid.Value ) )
            {
                entity.LastUpdated = DateTime.Now;
            }

            table.Upsert( entity );
        }

        public void Remove( Articulation articulation )
        {
            using var db = OpenDatabase();
            var table = db.GetCollection<ArticulationModel>( ArticulationsTableName );

            table.Delete( new ObjectId( articulation.Guid.Value ) );

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

        public IReadOnlyList<Articulation> All()
        {
            using var db = OpenDatabase();
            var table = db.GetCollection<ArticulationModel>( ArticulationsTableName );

            return CreateEntities( table.FindAll() );
        }

        public IEnumerable<Articulation> Find( DeveloperName developerName )
        {
            using var db = OpenDatabase();
            var table = db.GetCollection<ArticulationModel>( ArticulationsTableName );

            return CreateEntities( table.Find( x => developerName.Equals( new DeveloperName( x.DeveloperName ) ) ) );
        }

        public IEnumerable<Articulation> Find( ProductName productName )
        {
            using var db = OpenDatabase();
            var table = db.GetCollection<ArticulationModel>( ArticulationsTableName );

            return CreateEntities( table.Find( x => productName.Equals( new ProductName( x.ProductName ) ) ) );
        }
    }
}