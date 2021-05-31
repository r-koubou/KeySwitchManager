using KeySwitchManager.Commons.Data;
using KeySwitchManager.Storage.Yaml.KeySwitches.Translators;

using NUnit.Framework;

namespace KeySwitchManager.Testing.Storage.Yaml.KeySwitches
{
    [TestFixture]
    public class KeySwitchImportTest
    {
        [Test]
        public void ConvertToEntityTest()
        {
            const string yamlText = "KeySwitches:\n- Id: 61d7d095-1518-42c8-8cf7-904133b60339\n  Author: Author\n  Description: Description\n  Created: 2021-05-24T17:40:22.4780000Z\n  LastUpdated: 2021-05-27T14:29:53.7950000Z\n  DeveloperName: DeveloperName\n  ProductName: ProductName\n  InstrumentName: E.Guitar\n  Articulations:\n  - Name: Power Chord\n    MidiMessage:\n      NoteOn:\n      - Channel: 1\n        Note: C2\n        Velocity: 100\n      ControlChange:\n      - Channel: 0\n        ControlNumber: 1\n        Data: 100\n      ProgramChange:\n      - Channel: 0\n        ProgramNumber: 34\n    ExtraData: {}\n  ExtraData:\n    extKey: extValue\n";
            var translator = new YamlKeySwitchImportTranslator();
            var entity = translator.Translate( new PlainText( yamlText ) );
        }
    }
}