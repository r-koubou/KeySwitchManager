using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Json.KeySwitches.Translations;

using NUnit.Framework;

namespace Json.KeySwitches.Testing.KeySwitches
{
    [TestFixture]
    public class JsonToEntityTest
    {
        [Test]
        public void ConvertToEntityTest()
        {
            const string jsonText = "{\"Author\":\"Author\",\"Id\":\"d374e5e0-3b95-4f68-be4c-746dd8077a53\",\"Description\":\"Description\",\"Created\":\"2020-09-16T07:04:52.657Z\",\"LastUpdated\":\"2020-09-16T07:04:52.657Z\",\"DeveloperName\":\"DeveloperName\",\"ProductName\":\"ProductName\",\"InstrumentName\":\"E.Guitar\",\"Articulations\":[{\"Name\":\"Power Chord\",\"Type\":1,\"Group\":0,\"Color\":0,\"MidiMessage\":{\"NoteOn\":[{\"Status\":144,\"Channel\":0,\"Data1\":1,\"Data2\":23}],\"ControlChange\":[{\"Status\":176,\"Channel\":0,\"Data1\":2,\"Data2\":34}],\"ProgramChange\":[{\"Status\":192,\"Channel\":3,\"Data1\":45,\"Data2\":0}]}}]}";
            var translator = new JsonModelToKeySwitch();
            var entity = translator.Translate( new PlainText( jsonText ) );
        }

        [Test]
        /* All  {"Author":"Author","Id":"d374e5e0-3b95-4f68-be4c-746dd8077a53","Description":"Description","Created":"2020-09-16T07:04:52.657Z","LastUpdated":"2020-09-16T07:04:52.657Z","DeveloperName":"DeveloperName","ProductName":"ProductName","InstrumentName":"E.Guitar","Articulations":[{"Name":"Power Chord","Type":1,"Group":0,"Color":0,"MidiMessage":{"NoteOn":[{"Status":144,"Channel":0,"Data1":1,"Data2":23}],"ControlChange":[{"Status":176,"Channel":0,"Data1":2,"Data2":34}],"ProgramChange":[{"Status":192,"Channel":3,"Data1":45,"Data2":0}]}}]} */
        /*GUID*/[TestCase(         "{\"Author\":\"Author\",\"Description\":\"Description\",\"Created\":\"2020-09-16T07:04:52.657Z\",\"LastUpdated\":\"2020-09-16T07:04:52.657Z\",\"DeveloperName\":\"DeveloperName\",\"ProductName\":\"ProductName\",\"InstrumentName\":\"E.Guitar\",\"Articulations\":[{\"Name\":\"Power Chord\",\"Type\":1,\"Group\":0,\"Color\":0,\"MidiMessage\":{\"NoteOn\":[{\"Status\":144,\"Channel\":0,\"Data1\":1,\"Data2\":23}],\"ControlChange\":[{\"Status\":176,\"Channel\":0,\"Data1\":2,\"Data2\":34}],\"ProgramChange\":[{\"Status\":192,\"Channel\":3,\"Data1\":45,\"Data2\":0}]}}]}")]
        /*Author*/[TestCase(       "{\"Id\":\"d374e5e0-3b95-4f68-be4c-746dd8077a53\",\"Description\":\"Description\",\"Created\":\"2020-09-16T07:04:52.657Z\",\"LastUpdated\":\"2020-09-16T07:04:52.657Z\",\"DeveloperName\":\"DeveloperName\",\"ProductName\":\"ProductName\",\"InstrumentName\":\"E.Guitar\",\"Articulations\":[{\"Name\":\"Power Chord\",\"Type\":1,\"Group\":0,\"Color\":0,\"MidiMessage\":{\"NoteOn\":[{\"Status\":144,\"Channel\":0,\"Data1\":1,\"Data2\":23}],\"ControlChange\":[{\"Status\":176,\"Channel\":0,\"Data1\":2,\"Data2\":34}],\"ProgramChange\":[{\"Status\":192,\"Channel\":3,\"Data1\":45,\"Data2\":0}]}}]}")]
        /*Description*/[TestCase(  "{\"Author\":\"Author\",\"Id\":\"d374e5e0-3b95-4f68-be4c-746dd8077a53\",\"Created\":\"2020-09-16T07:04:52.657Z\",\"LastUpdated\":\"2020-09-16T07:04:52.657Z\",\"DeveloperName\":\"DeveloperName\",\"ProductName\":\"ProductName\",\"InstrumentName\":\"E.Guitar\",\"Articulations\":[{\"Name\":\"Power Chord\",\"Type\":1,\"Group\":0,\"Color\":0,\"MidiMessage\":{\"NoteOn\":[{\"Status\":144,\"Channel\":0,\"Data1\":1,\"Data2\":23}],\"ControlChange\":[{\"Status\":176,\"Channel\":0,\"Data1\":2,\"Data2\":34}],\"ProgramChange\":[{\"Status\":192,\"Channel\":3,\"Data1\":45,\"Data2\":0}]}}]}")]
        /*Date*/[TestCase(         "{\"Author\":\"Author\",\"Id\":\"d374e5e0-3b95-4f68-be4c-746dd8077a53\",\"Description\":\"Description\",\"DeveloperName\":\"DeveloperName\",\"ProductName\":\"ProductName\",\"InstrumentName\":\"E.Guitar\",\"Articulations\":[{\"Name\":\"Power Chord\",\"Type\":1,\"Group\":0,\"Color\":0,\"MidiMessage\":{\"NoteOn\":[{\"Status\":144,\"Channel\":0,\"Data1\":1,\"Data2\":23}],\"ControlChange\":[{\"Status\":176,\"Channel\":0,\"Data1\":2,\"Data2\":34}],\"ProgramChange\":[{\"Status\":192,\"Channel\":3,\"Data1\":45,\"Data2\":0}]}}]}")]
        /*Articulation*/[TestCase( "{\"Author\":\"Author\",\"Id\":\"d374e5e0-3b95-4f68-be4c-746dd8077a53\",\"Description\":\"Description\",\"Created\":\"2020-09-16T07:04:52.657Z\",\"LastUpdated\":\"2020-09-16T07:04:52.657Z\",\"DeveloperName\":\"DeveloperName\",\"ProductName\":\"ProductName\",\"InstrumentName\":\"E.Guitar\"}")]
        /*MIDI*/[TestCase(         "{\"Author\":\"Author\",\"Id\":\"d374e5e0-3b95-4f68-be4c-746dd8077a53\",\"Description\":\"Description\",\"Created\":\"2020-09-16T07:04:52.657Z\",\"LastUpdated\":\"2020-09-16T07:04:52.657Z\",\"DeveloperName\":\"DeveloperName\",\"ProductName\":\"ProductName\",\"InstrumentName\":\"E.Guitar\",\"Articulations\":[{\"Name\":\"Power Chord\",\"Type\":1,\"Group\":0,\"Color\":0}]}")]
        public void ConvertToEntityWithoutTest( string jsonText )
        {
            var translator = new JsonModelToKeySwitch();
            var entity = translator.Translate( new PlainText( jsonText ) );
        }

    }
}