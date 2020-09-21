using System.Collections.Generic;
using System.IO;

using Databases.LiteDB.KeySwitches.KeySwitches;

using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Interactors.KeySwitches.Adding;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Adding;

using NUnit.Framework;

namespace KeySwitchManager.Interactors.Testing.KeySwitches
{
    [TestFixture]
    public class AddingInteractorTest
    {
        [Test]
        public void AddingTest()
        {
            var inputData = new KeySwitchAddingRequest(
                "Author",
                "Description",
                "Developer",
                "Product",
                "E.Guitar",
                "Power Chord",
                ArticulationType.Direction,
                0,
                0,
                new List<MidiNoteOn>(),
                new List<MidiControlChange>(),
                new List<MidiProgramChange>()
            );

            var presenter = new IAddingPresenter.Null();
            var repository = new LiteDbKeySwitchRepository( new MemoryStream() );
            var interactor = new AddingInteractor( repository, presenter );

            interactor.Execute( inputData );

            Assert.AreEqual( 1, repository.Count() );
        }
    }
}