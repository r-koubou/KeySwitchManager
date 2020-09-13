using ArticulationManager.Json.Articulations.Model;
using ArticulationManager.Json.Articulations.Service;

using Newtonsoft.Json;

using NUnit.Framework;

namespace Json.Testing
{
    [TestFixture]
    public class JsonToEntityTest
    {
        [Test]
        public void ConvertToEntityTest()
        {
            const string jsonText = "{\"id\":\"f5bc8a19-4efc-47e1-86e7-d9a24ddef2f6\",\"created\":\"2020-09-12T17:16:26\",\"last_updated\":\"2020-09-12T17:16:26\",\"developer_name\":\"DeveloperName\",\"product_name\":\"ProductName\",\"articulation_name\":\"Power Chord\",\"articulation_type\":1,\"articulation_group\":0,\"articulation_color\":0,\"midi_message\":{\"note_on\":[{\"status\":144,\"channel\":0,\"data_byte_1\":1,\"data_byte_2\":23}],\"control_change\":[{\"status\":176,\"channel\":0,\"data_byte_1\":2,\"data_byte_2\":34}],\"program_change\":[{\"status\":192,\"channel\":3,\"data_byte_1\":45,\"data_byte_2\":0}]}}";
            var model = JsonConvert.DeserializeObject<ArticulationModel>( jsonText );
            var translator = new ArticulationModelTranslationService();
            var entity = translator.Translate( model );
        }
        [Test]
        public void ConvertToEntityWithoutGuidTest()
        {
            const string jsonText = "{\"created\":\"2020-09-12T17:16:26\",\"last_updated\":\"2020-09-12T17:16:26\",\"developer_name\":\"DeveloperName\",\"product_name\":\"ProductName\",\"articulation_name\":\"Power Chord\",\"articulation_type\":1,\"articulation_group\":0,\"articulation_color\":0,\"midi_message\":{\"note_on\":[{\"status\":144,\"channel\":0,\"data_byte_1\":1,\"data_byte_2\":23}],\"control_change\":[{\"status\":176,\"channel\":0,\"data_byte_1\":2,\"data_byte_2\":34}],\"program_change\":[{\"status\":192,\"channel\":3,\"data_byte_1\":45,\"data_byte_2\":0}]}}";
            var model = JsonConvert.DeserializeObject<ArticulationModel>( jsonText );
            var translator = new ArticulationModelTranslationService();
            var entity = translator.Translate( model );
        }

        [Test]
        public void ConvertToEntityWithoutDateTest()
        {
            const string jsonText = "{\"id\":\"f5bc8a19-4efc-47e1-86e7-d9a24ddef2f6\",\"developer_name\":\"DeveloperName\",\"product_name\":\"ProductName\",\"articulation_name\":\"Power Chord\",\"articulation_type\":1,\"articulation_group\":0,\"articulation_color\":0,\"midi_message\":{\"note_on\":[{\"status\":144,\"channel\":0,\"data_byte_1\":1,\"data_byte_2\":23}],\"control_change\":[{\"status\":176,\"channel\":0,\"data_byte_1\":2,\"data_byte_2\":34}],\"program_change\":[{\"status\":192,\"channel\":3,\"data_byte_1\":45,\"data_byte_2\":0}]}}";
            var model = JsonConvert.DeserializeObject<ArticulationModel>( jsonText );
            var translator = new ArticulationModelTranslationService();
            var entity = translator.Translate( model );
        }

        [Test]
        public void ConvertToEntityWithoutMidiListTest()
        {
            const string jsonText = "{\"id\":\"f5bc8a19-4efc-47e1-86e7-d9a24ddef2f6\",\"created\":\"2020-09-12T17:16:26.256Z\",\"last_updated\":\"2020-09-12T17:16:26.256Z\",\"developer_name\":\"DeveloperName\",\"product_name\":\"ProductName\",\"articulation_name\":\"Power Chord\",\"articulation_type\":1,\"articulation_group\":0,\"articulation_color\":0,\"midi_message\":{}}";
            var model = JsonConvert.DeserializeObject<ArticulationModel>( jsonText );
            var translator = new ArticulationModelTranslationService();
            var entity = translator.Translate( model );
        }

    }
}