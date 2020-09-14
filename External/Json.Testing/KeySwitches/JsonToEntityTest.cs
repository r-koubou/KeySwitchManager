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
            const string jsonText = "{\"id\":\"064ac60f-863a-4ec6-bdb9-a84dac2c2fa4\",\"created\":\"2020-09-14T14:01:18.669Z\",\"last_updated\":\"2020-09-14T14:01:18.669Z\",\"developer_name\":\"DeveloperName\",\"product_name\":\"ProductName\",\"instrument_name\":\"E.Guitar\",\"articulation\":[{\"articulation_name\":\"Power Chord\",\"articulation_type\":1,\"articulation_group\":0,\"articulation_color\":0,\"midi_message\":{\"note_on\":[{\"status\":144,\"channel\":0,\"data_byte_1\":1,\"data_byte_2\":23}],\"control_change\":[{\"status\":176,\"channel\":0,\"data_byte_1\":2,\"data_byte_2\":34}],\"program_change\":[{\"status\":192,\"channel\":3,\"data_byte_1\":45,\"data_byte_2\":0}]}}]}";
            var translator = new JsonModelToEntity();
            var entity = translator.Translate( new PlainText( jsonText ) );
        }
        [Test]
        public void ConvertToEntityWithoutGuidTest()
        {
            const string jsonText = "{\"created\":\"2020-09-14T14:01:18.669Z\",\"last_updated\":\"2020-09-14T14:01:18.669Z\",\"developer_name\":\"DeveloperName\",\"product_name\":\"ProductName\",\"instrument_name\":\"E.Guitar\",\"articulation\":[{\"articulation_name\":\"Power Chord\",\"articulation_type\":1,\"articulation_group\":0,\"articulation_color\":0,\"midi_message\":{\"note_on\":[{\"status\":144,\"channel\":0,\"data_byte_1\":1,\"data_byte_2\":23}],\"control_change\":[{\"status\":176,\"channel\":0,\"data_byte_1\":2,\"data_byte_2\":34}],\"program_change\":[{\"status\":192,\"channel\":3,\"data_byte_1\":45,\"data_byte_2\":0}]}}]}";
            var translator = new JsonModelToEntity();
            var entity = translator.Translate( new PlainText( jsonText ) );
        }

        [Test]
        public void ConvertToEntityWithoutDateTest()
        {
            const string jsonText = "{\"id\":\"064ac60f-863a-4ec6-bdb9-a84dac2c2fa4\",\"developer_name\":\"DeveloperName\",\"product_name\":\"ProductName\",\"instrument_name\":\"E.Guitar\",\"articulation\":[{\"articulation_name\":\"Power Chord\",\"articulation_type\":1,\"articulation_group\":0,\"articulation_color\":0,\"midi_message\":{\"note_on\":[{\"status\":144,\"channel\":0,\"data_byte_1\":1,\"data_byte_2\":23}],\"control_change\":[{\"status\":176,\"channel\":0,\"data_byte_1\":2,\"data_byte_2\":34}],\"program_change\":[{\"status\":192,\"channel\":3,\"data_byte_1\":45,\"data_byte_2\":0}]}}]}";
            var translator = new JsonModelToEntity();
            var entity = translator.Translate( new PlainText( jsonText ) );
        }

        [Test]
        public void ConvertToEntityWithoutArticulationTest()
        {
            const string jsonText = "{\"id\":\"064ac60f-863a-4ec6-bdb9-a84dac2c2fa4\",\"created\":\"2020-09-14T14:01:18.669Z\",\"last_updated\":\"2020-09-14T14:01:18.669Z\",\"developer_name\":\"DeveloperName\",\"product_name\":\"ProductName\",\"instrument_name\":\"E.Guitar\"}";
            var translator = new JsonModelToEntity();
            var entity = translator.Translate( new PlainText( jsonText ) );
        }

        [Test]
        public void ConvertToEntityWithoutMidiListTest()
        {
            const string jsonText = "{\"id\":\"064ac60f-863a-4ec6-bdb9-a84dac2c2fa4\",\"created\":\"2020-09-14T14:01:18.669Z\",\"last_updated\":\"2020-09-14T14:01:18.669Z\",\"developer_name\":\"DeveloperName\",\"product_name\":\"ProductName\",\"instrument_name\":\"E.Guitar\",\"articulation\":[{\"articulation_name\":\"Power Chord\",\"articulation_type\":1,\"articulation_group\":0,\"articulation_color\":0}]}";
            var translator = new JsonModelToEntity();
            var entity = translator.Translate( new PlainText( jsonText ) );
        }

    }
}