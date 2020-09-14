using System;
using System.IO;
using System.Linq;

using ArticulationManager.Common.Testing;
using ArticulationManager.Databases.LiteDB.Articulations;
using ArticulationManager.Domain.Articulations.Value;
using ArticulationManager.Interactors.Articulations.Importing.Text;
using ArticulationManager.Json.Articulations.Translations;
using ArticulationManager.Presenters.Articulations;
using ArticulationManager.UseCases.Articulations.Importing.Text;

using NUnit.Framework;

namespace ArticulationManager.Interactors.Testing.Articulations
{
    [TestFixture]
    public class ImportingJsonInteractorTest
    {
        [Test]
        public void ImportTest()
        {
            var jsonText = "{\"id\":\"064ac60f-863a-4ec6-bdb9-a84dac2c2fa4\",\"created\":\"2020-09-14T14:01:18.669Z\",\"last_updated\":\"2020-09-14T14:01:18.669Z\",\"developer_name\":\"DeveloperName\",\"product_name\":\"ProductName\",\"instrument_name\":\"E.Guitar\",\"articulation\":[{\"articulation_name\":\"Power Chord\",\"articulation_type\":1,\"articulation_group\":0,\"articulation_color\":0,\"midi_message\":{\"note_on\":[{\"status\":144,\"channel\":1,\"data_byte_1\":1,\"data_byte_2\":23}],\"control_change\":[{\"status\":176,\"channel\":2,\"data_byte_1\":2,\"data_byte_2\":34}],\"program_change\":[{\"status\":192,\"channel\":3,\"data_byte_1\":45,\"data_byte_2\":0}]}}]}";

            var dbRepository = new LiteDbKeySwitchRepository( new MemoryStream() );
            var inputData = new InputData( jsonText );
            var translator = new JsonModelToEntity();
            var interactor = new ImportingJsonInteractor(
                dbRepository,
                new IImportingTextPresenter.Console(),
                translator
            );

            interactor.Execute( inputData );

            var entities = dbRepository.Find( new DeveloperName( "DeveloperName" ) );
            var cmp = translator.Translate( inputData.JsonText );

            Assert.AreEqual( entities.First(), cmp );

        }
    }
}