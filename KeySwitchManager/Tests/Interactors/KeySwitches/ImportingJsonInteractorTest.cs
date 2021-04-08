using System.IO;
using System.Linq;

using Database.LiteDB.KeySwitches;

using KeySwitchManager.Domain.KeySwitches.Values;
using KeySwitchManager.Interactors.KeySwitches.Importing;
using KeySwitchManager.Json.KeySwitches.Translators;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Importing.Text;

using NUnit.Framework;

namespace KeySwitchManager.Interactors.Testing.KeySwitches
{
    [TestFixture]
    public class ImportingJsonInteractorTest
    {
        [Test]
        public void ImportTest()
        {
            var jsonText = "[{\"Author\":\"Author\",\"Id\":\"d374e5e0-3b95-4f68-be4c-746dd8077a53\",\"Description\":\"Description\",\"Created\":\"2020-09-16T07:04:52.657Z\",\"LastUpdated\":\"2020-09-16T07:04:52.657Z\",\"DeveloperName\":\"DeveloperName\",\"ProductName\":\"ProductName\",\"InstrumentName\":\"E.Guitar\",\"Articulations\":[{\"Name\":\"Power Chord\",\"Type\":1,\"Group\":0,\"Color\":0,\"MidiMessage\":{\"NoteOn\":[{\"Status\":144,\"Channel\":0,\"Data1\":1,\"Data2\":23}],\"ControlChange\":[{\"Status\":176,\"Channel\":0,\"Data1\":2,\"Data2\":34}],\"ProgramChange\":[{\"Status\":192,\"Channel\":3,\"Data1\":45,\"Data2\":0}]}}]}]";

            var dbRepository = new LiteDbKeySwitchRepository( new MemoryStream() );
            var inputData = new ImportingTextRequest( jsonText );
            var translator = new JsonModelListToKeySwitchList();
            var interactor = new ImportingJsonInteractor(
                dbRepository,
                translator, new IImportingTextPresenter.Console()
            );

            interactor.Execute( inputData );

            var entities = dbRepository.Find( new DeveloperName( "DeveloperName" ) );
            var cmp = translator.Translate( inputData.JsonText );

            var keySwitches = entities.ToList();
            var cmpList = cmp.ToList();

            Assert.AreEqual( keySwitches.Count(), cmpList.Count() );
            Assert.AreEqual( keySwitches.First(), cmpList.First() );

        }
    }
}