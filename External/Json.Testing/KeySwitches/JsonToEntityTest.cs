using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Json.KeySwitches.Translations;

using NUnit.Framework;

namespace Json.Testing.KeySwitches
{
    [TestFixture]
    public class JsonToEntityTest
    {
        [Test]
        public void ConvertToEntityTest()
        {
            const string jsonText = "{\"id\":\"d374e5e0-3b95-4f68-be4c-746dd8077a53\",\"author\":\"Author\",\"description\":\"Description\",\"created\":\"2020-09-16T07:04:52.657Z\",\"last_updated\":\"2020-09-16T07:04:52.657Z\",\"developer_name\":\"DeveloperName\",\"product_name\":\"ProductName\",\"instrument_name\":\"E.Guitar\",\"articulations\":[{\"name\":\"Power Chord\",\"type\":1,\"group\":0,\"color\":0,\"midi_message\":{\"note_on\":[{\"status\":144,\"channel\":0,\"data1\":1,\"data2\":23}],\"control_change\":[{\"status\":176,\"channel\":0,\"data1\":2,\"data2\":34}],\"program_change\":[{\"status\":192,\"channel\":3,\"data1\":45,\"data2\":0}]}}]}";
            var translator = new JsonModelToKeySwitch();
            var entity = translator.Translate( new PlainText( jsonText ) );
        }

        [Test]
        /*GUID*/[TestCase(         "{\"author\":\"Author\",\"description\":\"Description\",\"created\":\"2020-09-16T07:04:52.657Z\",\"last_updated\":\"2020-09-16T07:04:52.657Z\",\"developer_name\":\"DeveloperName\",\"product_name\":\"ProductName\",\"instrument_name\":\"E.Guitar\",\"articulations\":[{\"name\":\"Power Chord\",\"type\":1,\"group\":0,\"color\":0,\"midi_message\":{\"note_on\":[{\"status\":144,\"channel\":0,\"data1\":1,\"data2\":23}],\"control_change\":[{\"status\":176,\"channel\":0,\"data1\":2,\"data2\":34}],\"program_change\":[{\"status\":192,\"channel\":3,\"data1\":45,\"data2\":0}]}}]}")]
        /*Author*/[TestCase(       "{\"id\":\"d374e5e0-3b95-4f68-be4c-746dd8077a53\",\"description\":\"Description\",\"created\":\"2020-09-16T07:04:52.657Z\",\"last_updated\":\"2020-09-16T07:04:52.657Z\",\"developer_name\":\"DeveloperName\",\"product_name\":\"ProductName\",\"instrument_name\":\"E.Guitar\",\"articulations\":[{\"name\":\"Power Chord\",\"type\":1,\"group\":0,\"color\":0,\"midi_message\":{\"note_on\":[{\"status\":144,\"channel\":0,\"data1\":1,\"data2\":23}],\"control_change\":[{\"status\":176,\"channel\":0,\"data1\":2,\"data2\":34}],\"program_change\":[{\"status\":192,\"channel\":3,\"data1\":45,\"data2\":0}]}}]}")]
        /*Description*/[TestCase(  "{\"id\":\"d374e5e0-3b95-4f68-be4c-746dd8077a53\",\"author\":\"Author\",\"created\":\"2020-09-16T07:04:52.657Z\",\"last_updated\":\"2020-09-16T07:04:52.657Z\",\"developer_name\":\"DeveloperName\",\"product_name\":\"ProductName\",\"instrument_name\":\"E.Guitar\",\"articulations\":[{\"name\":\"Power Chord\",\"type\":1,\"group\":0,\"color\":0,\"midi_message\":{\"note_on\":[{\"status\":144,\"channel\":0,\"data1\":1,\"data2\":23}],\"control_change\":[{\"status\":176,\"channel\":0,\"data1\":2,\"data2\":34}],\"program_change\":[{\"status\":192,\"channel\":3,\"data1\":45,\"data2\":0}]}}]}")]
        /*Date*/[TestCase(         "{\"id\":\"d374e5e0-3b95-4f68-be4c-746dd8077a53\",\"author\":\"Author\",\"description\":\"Description\",\"developer_name\":\"DeveloperName\",\"product_name\":\"ProductName\",\"instrument_name\":\"E.Guitar\",\"articulations\":[{\"name\":\"Power Chord\",\"type\":1,\"group\":0,\"color\":0,\"midi_message\":{\"note_on\":[{\"status\":144,\"channel\":0,\"data1\":1,\"data2\":23}],\"control_change\":[{\"status\":176,\"channel\":0,\"data1\":2,\"data2\":34}],\"program_change\":[{\"status\":192,\"channel\":3,\"data1\":45,\"data2\":0}]}}]}")]
        /*Articulation*/[TestCase( "{\"id\":\"d374e5e0-3b95-4f68-be4c-746dd8077a53\",\"author\":\"Author\",\"description\":\"Description\",\"created\":\"2020-09-16T07:04:52.657Z\",\"last_updated\":\"2020-09-16T07:04:52.657Z\",\"developer_name\":\"DeveloperName\",\"product_name\":\"ProductName\",\"instrument_name\":\"E.Guitar\"}")]
        /*MIDI*/[TestCase(         "{\"id\":\"d374e5e0-3b95-4f68-be4c-746dd8077a53\",\"author\":\"Author\",\"description\":\"Description\",\"created\":\"2020-09-16T07:04:52.657Z\",\"last_updated\":\"2020-09-16T07:04:52.657Z\",\"developer_name\":\"DeveloperName\",\"product_name\":\"ProductName\",\"instrument_name\":\"E.Guitar\",\"articulations\":[{\"name\":\"Power Chord\",\"type\":1,\"group\":0,\"color\":0}]}")]
        public void ConvertToEntityWithoutTest( string jsonText )
        {
            var translator = new JsonModelToKeySwitch();
            var entity = translator.Translate( new PlainText( jsonText ) );
        }

    }
}