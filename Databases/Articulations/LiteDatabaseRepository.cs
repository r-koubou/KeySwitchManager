using System.Collections.Generic;

using ArticulationManager.Databases.Articulations.Model;
using ArticulationManager.Databases.Articulations.Service;
using ArticulationManager.Domain.Articulations.Aggregate;
using ArticulationManager.Domain.Articulations.Value;
using ArticulationManager.Domain.Commons;
using ArticulationManager.Gateways.Articulations;

using LiteDB;

namespace ArticulationManager.Databases.Articulations
{
    public class LiteDatabaseRepository : IArticulationRepository
    {
        private const string TableName = @"articulations";

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
            var table = db.GetCollection<ArticulationModel>( TableName );

            if( table.Exists( x => x.Id == articulation.Id.Value ) )
            {}
            else
            {
                var translator = new ArticulationTranslationService();
                table.Insert( translator.Translate( articulation ) );
            }
        }

        public void Remove( Articulation articulation )
        {
            using var db = OpenDatabase();
            var table = db.GetCollection<ArticulationModel>( TableName );

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

        public IReadOnlyList<Articulation> All()
        {
            using var db = OpenDatabase();

            var table = db.GetCollection<ArticulationModel>( TableName );
            return CreateEntities( table.FindAll() );
        }

        public IEnumerable<Articulation> Find( EntityId id )
        {
            using var db = OpenDatabase();
            var table = db.GetCollection<ArticulationModel>( TableName );

            return CreateEntities( table.Find(x => id.Equals( new EntityId( x.Id ) ) ) );
        }

        public IEnumerable<Articulation> Find( DeveloperName developerName )
        {
            using var db = OpenDatabase();
            var table = db.GetCollection<ArticulationModel>( TableName );

            return CreateEntities( table.Find( x => developerName.Equals( new DeveloperName( x.DeveloperName ) ) ) );
        }

        public IEnumerable<Articulation> Find( ProductName productName )
        {
            using var db = OpenDatabase();
            var table = db.GetCollection<ArticulationModel>( TableName );

            return CreateEntities( table.Find( x => productName.Equals( new ProductName( x.ProductName ) ) ) );
        }
    }
}