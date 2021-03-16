using System.Collections.Generic;
using System.IO;

using Databases.LiteDB.KeySwitches.KeySwitches;

using KeySwitchManager.Domain.MidiMessages.Entity;
using KeySwitchManager.Interactors.KeySwitch.Adding;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitch.Adding;

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
                new List<MidiNoteOn>(),
                new List<MidiControlChange>(),
                new List<MidiProgramChange>(),
                new Dictionary<string, string>()
            );

            var presenter = new IAddingPresenter.Null();
            var repository = new LiteDbKeySwitchRepository( new MemoryStream() );
            var interactor = new AddingInteractor( repository, presenter );

            interactor.Execute( inputData );

            Assert.AreEqual( 1, repository.Count() );
        }
    }
}