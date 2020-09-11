using System;
using System.Collections.Generic;
using System.IO;

using ArticulationManager.Databases.LiteDB.Articulations;
using ArticulationManager.Domain.Articulations.Aggregate;
using ArticulationManager.Domain.Articulations.Value;
using ArticulationManager.Domain.Commons;
using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.Services;
using ArticulationManager.Interactors.Articulations.Database;
using ArticulationManager.Presenters.Articulations.Database;
using ArticulationManager.UseCases.Articulations.Database.Removing;

using NUnit.Framework;

namespace ArticulationManager.Interactors.Testing.Articulations.Database
{
    [TestFixture]
    public class RemovingInteractorTest
    {
        [Test]
        public void AddingTest()
        {
            const string developerName = "Developer";
            const string productName = "Product";

            var inputData = new InputData(
                developerName,
                productName
            );

            var presenter = new IRemovingArticulationPresenter.Null();
            var repository = new LiteDbArticulationRepository( new MemoryStream() );
            var interactor = new RemovingArticulationInteractor( repository, presenter );

            #region Adding Test data for removing
            var now = DateTime.Now;
            var entity = new Articulation(
                new EntityGuid( Guid.NewGuid() ),
                EntityDateTimeService.FromDateTime( now ),
                EntityDateTimeService.FromDateTime( now ),
                new DeveloperName( developerName ),
                new ProductName( productName ),
                new ArticulationName( "Power Chord" ),
                ArticulationType.Default,
                new ArticulationGroup( 0 ),
                new ArticulationColor( 0 ),
                new List<NoteOn>(),
                new List<ControlChange>(),
                new List<ProgramChange>()
            );
            repository.Save( entity );
            #endregion
            Assert.AreEqual( 1, repository.Count() );

            interactor.Execute( inputData );

            Assert.AreEqual( 0, repository.Count() );
        }
    }
}