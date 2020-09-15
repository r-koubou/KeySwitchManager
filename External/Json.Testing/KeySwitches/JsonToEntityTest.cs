using ArticulationManager.Domain.Commons;
using ArticulationManager.Json.KeySwitches.Translations;

using NUnit.Framework;

namespace Json.Testing.KeySwitches
{
    [TestFixture]
    public class JsonToEntityTest
    {
        [Test]
        public void ConvertToEntityTest()
        {
            const string jsonText = "{\"id\":\"419db555-cc1d-405c-8b28-281ded630a45\",\"author\":\"Author\",\"description\":\"Description\",\"created\":\"2020-09-15T16:21:11.354Z\",\"last_updated\":\"2020-09-15T16:21:11.354Z\",\"developer_name\":\"DeveloperName\",\"product_name\":\"ProductName\",\"instrument_name\":\"E.Guitar\",\"articulation\":[{\"articulation_name\":\"Power Chord\",\"articulation_type\":1,\"articulation_group\":0,\"articulation_color\":0,\"midi_message\":{\"note_on\":[],\"control_change\":[],\"program_change\":[]}}]}";
            var translator = new JsonModelToEntity();
            var entity = translator.Translate( new PlainText( jsonText ) );
        }

        [Test]
        /*GUID*/[TestCase(         "{\"author\":\"Author\",\"description\":\"Description\",\"created\":\"2020-09-15T16:21:11.354Z\",\"last_updated\":\"2020-09-15T16:21:11.354Z\",\"developer_name\":\"DeveloperName\",\"product_name\":\"ProductName\",\"instrument_name\":\"E.Guitar\",\"articulation\":[{\"articulation_name\":\"Power Chord\",\"articulation_type\":1,\"articulation_group\":0,\"articulation_color\":0,\"midi_message\":{\"note_on\":[],\"control_change\":[],\"program_change\":[]}}]}")]
        /*Author*/[TestCase(       "{\"id\":\"419db555-cc1d-405c-8b28-281ded630a45\",\"description\":\"Description\",\"created\":\"2020-09-15T16:21:11.354Z\",\"last_updated\":\"2020-09-15T16:21:11.354Z\",\"developer_name\":\"DeveloperName\",\"product_name\":\"ProductName\",\"instrument_name\":\"E.Guitar\",\"articulation\":[{\"articulation_name\":\"Power Chord\",\"articulation_type\":1,\"articulation_group\":0,\"articulation_color\":0,\"midi_message\":{\"note_on\":[],\"control_change\":[],\"program_change\":[]}}]}")]
        /*Description*/[TestCase(  "{\"id\":\"419db555-cc1d-405c-8b28-281ded630a45\",\"author\":\"Author\",\"created\":\"2020-09-15T16:21:11.354Z\",\"last_updated\":\"2020-09-15T16:21:11.354Z\",\"developer_name\":\"DeveloperName\",\"product_name\":\"ProductName\",\"instrument_name\":\"E.Guitar\",\"articulation\":[{\"articulation_name\":\"Power Chord\",\"articulation_type\":1,\"articulation_group\":0,\"articulation_color\":0,\"midi_message\":{\"note_on\":[],\"control_change\":[],\"program_change\":[]}}]}")]
        /*Date*/[TestCase(         "{\"id\":\"419db555-cc1d-405c-8b28-281ded630a45\",\"author\":\"Author\",\"description\":\"Description\",\"developer_name\":\"DeveloperName\",\"product_name\":\"ProductName\",\"instrument_name\":\"E.Guitar\",\"articulation\":[{\"articulation_name\":\"Power Chord\",\"articulation_type\":1,\"articulation_group\":0,\"articulation_color\":0,\"midi_message\":{\"note_on\":[],\"control_change\":[],\"program_change\":[]}}]}")]
        /*Articulation*/[TestCase( "{\"id\":\"419db555-cc1d-405c-8b28-281ded630a45\",\"author\":\"Author\",\"description\":\"Description\",\"created\":\"2020-09-15T16:21:11.354Z\",\"last_updated\":\"2020-09-15T16:21:11.354Z\",\"developer_name\":\"DeveloperName\",\"product_name\":\"ProductName\",\"instrument_name\":\"E.Guitar\"}")]
        /*MIDI*/[TestCase(         "{\"id\":\"419db555-cc1d-405c-8b28-281ded630a45\",\"author\":\"Author\",\"description\":\"Description\",\"created\":\"2020-09-15T16:21:11.354Z\",\"last_updated\":\"2020-09-15T16:21:11.354Z\",\"developer_name\":\"DeveloperName\",\"product_name\":\"ProductName\",\"instrument_name\":\"E.Guitar\",\"articulation\":[{\"articulation_name\":\"Power Chord\",\"articulation_type\":1,\"articulation_group\":0,\"articulation_color\":0}]}")]
        public void ConvertToEntityWithoutTest( string jsonText )
        {
            var translator = new JsonModelToEntity();
            var entity = translator.Translate( new PlainText( jsonText ) );
        }

    }
}