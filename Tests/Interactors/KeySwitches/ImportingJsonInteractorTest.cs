using System.IO;
using System.Linq;

using Databases.LiteDB.KeySwitches.KeySwitches;

using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Interactors.KeySwitches.Importing;
using KeySwitchManager.Json.KeySwitches.Translations;
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
            var jsonText = "[{\"id\":\"419db555-cc1d-405c-8b28-281ded630a45\",\"author\":\"Author\",\"description\":\"Description\",\"created\":\"2020-09-15T16:21:11.354Z\",\"last_updated\":\"2020-09-15T16:21:11.354Z\",\"developer_name\":\"DeveloperName\",\"product_name\":\"ProductName\",\"instrument_name\":\"E.Guitar\",\"articulation\":[{\"articulation_name\":\"Power Chord\",\"articulation_type\":1,\"articulation_group\":0,\"articulation_color\":0,\"midi_message\":{\"note_on\":[],\"control_change\":[],\"program_change\":[]}}]}]";

            var dbRepository = new LiteDbKeySwitchRepository( new MemoryStream() );
            var inputData = new ImportingTextRequest( jsonText );
            var translator = new JsonModelListToKeySwitchList();
            var interactor = new ImportingJsonInteractor(
                dbRepository,
                translator, new IImportingTextPresenter.Console()
            );

            interactor.Execute( inputData );

            var entities = dbRepository.Find( IDeveloperNameFactory.Default.Create( "DeveloperName" ) );
            var cmp = translator.Translate( inputData.JsonText );

            var keySwitches = entities.ToList();
            var cmpList = cmp.ToList();

            Assert.AreEqual( keySwitches.Count(), cmpList.Count() );
            Assert.AreEqual( keySwitches.First(), cmpList.First() );

        }
    }
}