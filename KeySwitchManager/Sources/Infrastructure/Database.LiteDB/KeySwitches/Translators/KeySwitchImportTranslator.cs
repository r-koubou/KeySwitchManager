using System.Collections.Generic;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Entities;
using KeySwitchManager.Domain.MidiMessages.Models;
using KeySwitchManager.Domain.MidiMessages.Models.Entities;
using KeySwitchManager.Infrastructure.Database.LiteDB.KeySwitches.Models;

namespace KeySwitchManager.Infrastructure.Database.LiteDB.KeySwitches.Translators
{
    internal class KeySwitchImportTranslator : IDataTranslator<KeySwitchModel, KeySwitch>
    {
        public KeySwitch Translate( KeySwitchModel source )
        {
            var articulations = new List<Articulation>();

            foreach( var i in source.Articulations )
            {
                var noteOn = new List<IMidiChannelVoiceMessage>();
                var controlChange = new List<IMidiChannelVoiceMessage>();
                var programChange = new List<IMidiChannelVoiceMessage>();

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
            ICollection<IMidiChannelVoiceMessage> dest,
            IMidiChannelVoiceMessageFactory<IMidiChannelVoiceMessage> messageFactory )
        {
            foreach( var i in src )
            {
                dest.Add(
                    messageFactory.Create(
                        i.Status & 0x0F,
                        i.DataByte1,
                        i.DataByte2
                    )
                );
            }
        }
    }
}