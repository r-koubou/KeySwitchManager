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
            const string yamlText = "KeySwitches:\n- Id: cf959393-517b-487b-a4d5-89adfd76394f\n  Author: Author\n  Description: Description\n  Created: 2021-05-19T17:20:17.8390000Z\n  LastUpdated: 2021-05-19T17:20:17.8390000Z\n  DeveloperName: DeveloperName\n  ProductName: ProductName\n  InstrumentName: E.Guitar\n  Articulations:\n  - Name: Power Chord\n    MidiMessage:\n      NoteOn:\n      - Status: 144\n        Channel: 0\n        Data1: 1\n        Data2: 23\n      ControlChange:\n      - Status: 176\n        Channel: 0\n        Data1: 2\n        Data2: 34\n      ProgramChange:\n      - Status: 195\n        Channel: 3\n        Data1: 45\n        Data2: 0\n  ExtraData:\n    extKey: extValue\n";
            var translator = new YamlKeySwitchImportTranslator();
            var entity = translator.Translate( new PlainText( yamlText ) );
        }

        [Test]
        /* All  "KeySwitches:\n- Id: cf959393-517b-487b-a4d5-89adfd76394f\n  Author: Author\n  Description: Description\n  Created: 2021-05-19T17:20:17.8390000Z\n  LastUpdated: 2021-05-19T17:20:17.8390000Z\n  DeveloperName: DeveloperName\n  ProductName: ProductName\n  InstrumentName: E.Guitar\n  Articulations:\n  - Name: Power Chord\n    MidiMessage:\n      NoteOn:\n      - Status: 144\n        Channel: 0\n        Data1: 1\n        Data2: 23\n      ControlChange:\n      - Status: 176\n        Channel: 0\n        Data1: 2\n        Data2: 34\n      ProgramChange:\n      - Status: 195\n        Channel: 3\n        Data1: 45\n        Data2: 0\n  ExtraData:\n    extKey: extValue\n" */
        /*GUID*/[TestCase(         "KeySwitches:\n- Author: Author\n  Description: Description\n  Created: 2021-05-19T17:20:17.8390000Z\n  LastUpdated: 2021-05-19T17:20:17.8390000Z\n  DeveloperName: DeveloperName\n  ProductName: ProductName\n  InstrumentName: E.Guitar\n  Articulations:\n  - Name: Power Chord\n    MidiMessage:\n      NoteOn:\n      - Status: 144\n        Channel: 0\n        Data1: 1\n        Data2: 23\n      ControlChange:\n      - Status: 176\n        Channel: 0\n        Data1: 2\n        Data2: 34\n      ProgramChange:\n      - Status: 195\n        Channel: 3\n        Data1: 45\n        Data2: 0\n  ExtraData:\n    extKey: extValue\n")]
        /*Author*/[TestCase(       "KeySwitches:\n- Id: cf959393-517b-487b-a4d5-89adfd76394f\n  Description: Description\n  Created: 2021-05-19T17:20:17.8390000Z\n  LastUpdated: 2021-05-19T17:20:17.8390000Z\n  DeveloperName: DeveloperName\n  ProductName: ProductName\n  InstrumentName: E.Guitar\n  Articulations:\n  - Name: Power Chord\n    MidiMessage:\n      NoteOn:\n      - Status: 144\n        Channel: 0\n        Data1: 1\n        Data2: 23\n      ControlChange:\n      - Status: 176\n        Channel: 0\n        Data1: 2\n        Data2: 34\n      ProgramChange:\n      - Status: 195\n        Channel: 3\n        Data1: 45\n        Data2: 0\n  ExtraData:\n    extKey: extValue\n")]
        /*Description*/[TestCase(  "KeySwitches:\n- Id: cf959393-517b-487b-a4d5-89adfd76394f\n  Author: Author\n  Created: 2021-05-19T17:20:17.8390000Z\n  LastUpdated: 2021-05-19T17:20:17.8390000Z\n  DeveloperName: DeveloperName\n  ProductName: ProductName\n  InstrumentName: E.Guitar\n  Articulations:\n  - Name: Power Chord\n    MidiMessage:\n      NoteOn:\n      - Status: 144\n        Channel: 0\n        Data1: 1\n        Data2: 23\n      ControlChange:\n      - Status: 176\n        Channel: 0\n        Data1: 2\n        Data2: 34\n      ProgramChange:\n      - Status: 195\n        Channel: 3\n        Data1: 45\n        Data2: 0\n  ExtraData:\n    extKey: extValue\n")]
        /*Date*/[TestCase(         "KeySwitches:\n- Id: cf959393-517b-487b-a4d5-89adfd76394f\n  Author: Author\n  Description: Description\n  DeveloperName: DeveloperName\n  ProductName: ProductName\n  InstrumentName: E.Guitar\n  Articulations:\n  - Name: Power Chord\n    MidiMessage:\n      NoteOn:\n      - Status: 144\n        Channel: 0\n        Data1: 1\n        Data2: 23\n      ControlChange:\n      - Status: 176\n        Channel: 0\n        Data1: 2\n        Data2: 34\n      ProgramChange:\n      - Status: 195\n        Channel: 3\n        Data1: 45\n        Data2: 0\n  ExtraData:\n    extKey: extValue\n")]
        /*Articulation*/[TestCase( "KeySwitches:\n- Id: cf959393-517b-487b-a4d5-89adfd76394f\n  Author: Author\n  Description: Description\n  Created: 2021-05-19T17:20:17.8390000Z\n  LastUpdated: 2021-05-19T17:20:17.8390000Z\n  DeveloperName: DeveloperName\n  ProductName: ProductName\n  InstrumentName: E.Guitar\n  ExtraData:\n    extKey: extValue\n")]
        /*MIDI*/[TestCase(         "KeySwitches:\n- Id: cf959393-517b-487b-a4d5-89adfd76394f\n  Author: Author\n  Description: Description\n  Created: 2021-05-19T17:20:17.8390000Z\n  LastUpdated: 2021-05-19T17:20:17.8390000Z\n  DeveloperName: DeveloperName\n  ProductName: ProductName\n  InstrumentName: E.Guitar\n  Articulations:\n  - Name: Power Chord\n  ExtraData:\n    extKey: extValue\n")]
        public void ConvertToEntityWithoutTest( string jsonText )
        {
            var translator = new YamlKeySwitchImportTranslator();
            var entity = translator.Translate( new PlainText( jsonText ) );
        }

    }
}