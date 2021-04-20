using System.Collections.Generic;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Midi.Models;
using KeySwitchManager.Domain.KeySwitches.Midi.Models.Entities;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Entities;
using KeySwitchManager.Infrastructures.Database.LiteDB.KeySwitches.Models;

namespace KeySwitchManager.Infrastructures.Database.LiteDB.KeySwitches.Translators
{
    internal class KeySwitchImportTranslator : IDataTranslator<KeySwitchModel, KeySwitch>
    {
        public KeySwitch Translate( KeySwitchModel source )
        {
            var articulations = new List<Articulation>();

            foreach( var i in source.Articulations )
            {
                var noteOn = new List<IMidiMessage>();
                var controlChange = new List<IMidiMessage>();
                var programChange = new List<IMidiMessage>();

                ConvertMessageList( i.NoteOn,        noteOn,        IMidiNoteOnFactory.Default );
                ConvertMessageList( i.ControlChange, controlChange, IMidiControlChangeFactory.Default );
                ConvertMessageList( i.ProgramChange, programChange, IMidiProgramChangeFactory.Default );

                var articulation = IArticulationFactory.Default.Create(
                    i.ArticulationName,
                    noteOn,
                    controlChange,
                    programChange,
                    ConvertExtraData( i.ExtraData )
                );

                articulations.Add( articulation );
            }

            return IKeySwitchFactory.Default.Create(
                source.Id,
                source.Author,
                source.Description,
                source.Created,
                source.LastUpdated,
                source.DeveloperName,
                source.ProductName,
                source.InstrumentName,
                articulations,
                ConvertExtraData( source.ExtraData )
            );

        }

        private static IReadOnlyDictionary<string, string> ConvertExtraData( IDictionary<string, object> source )
        {
            var extra = new Dictionary<string, string>();

            foreach( var keyValuePair in source )
            {
                var k = keyValuePair.Key;
                var v = keyValuePair.Value;
                extra.Add( k, v.ToString()! );
            }

            return extra;
        }

        private static void ConvertMessageList(
            IEnumerable<MidiMessageModel> src,
            ICollection<IMidiMessage> dest,
            IMidiMessageFactory<IMidiMessage> messageFactory )
        {
            foreach( var i in src )
            {
                dest.Add(
                    messageFactory.Create(
                        i.Status,
                        i.DataByte1,
                        i.DataByte2
                    )
                );
            }
        }
    }
}