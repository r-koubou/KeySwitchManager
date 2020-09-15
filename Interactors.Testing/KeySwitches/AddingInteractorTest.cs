using System.Collections.Generic;
using System.IO;

using ArticulationManager.Databases.LiteDB.KeySwitches;
using ArticulationManager.Domain.KeySwitches.Value;
using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Interactors.KeySwitches.Adding;
using ArticulationManager.Presenters.KeySwitches;
using ArticulationManager.UseCases.KeySwitches.Adding;

using NUnit.Framework;

namespace ArticulationManager.Interactors.Testing.KeySwitches
{
    [TestFixture]
    public class AddingInteractorTest
    {
        [Test]
        public void AddingTest()
        {
            var inputData = new InputData(
                "Author",
                "Description",
                "Developer",
                "Product",
                "E.Guitar",
                "Power Chord",
                ArticulationType.Direction,
                0,
                0,
                new List<NoteOn>(),
                new List<ControlChange>(),
                new List<ProgramChange>()
            );

            var presenter = new IAddingPresenter.Null();
            var repository = new LiteDbKeySwitchRepository( new MemoryStream() );
            var interactor = new AddingInteractor( repository, presenter );

            interactor.Execute( inputData );

            Assert.AreEqual( 1, repository.Count() );
        }
    }
}