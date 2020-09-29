using System.Collections.Generic;

using Databases.LiteDB.KeySwitches.KeySwitches.Models;

using KeySwitchManager.Common.Utilities;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Domain.MidiMessages;
using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.Translations;

namespace Databases.LiteDB.KeySwitches.KeySwitches.Translations
{
    public class DbModelToEntity : IDataTranslator<KeySwitchModel, KeySwitch>
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
                    EnumHelper.Parse<ArticulationType>( i.ArticulationType ),
                    i.ArticulationGroup,
                    i.ArticulationColor,
                    noteOn,
                    controlChange,
                    programChange
                );

                articulations.Add( articulation );
            }

            var extra = new Dictionary<string, string>();
            foreach( var key in source.ExtraData.Keys )
            {
                var v = source.ExtraData[ key ].AsString;
                extra.Add( key, v );
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
                extra
            );

        }

        private static void ConvertMessageList(
            IEnumerable<MidiMessageModel> src,
            ICollection<IMidiMessage> dest,
            IMidiMessageFactory messageFactory )
        {
            foreach( var i in src )
            {
                dest.Add(
                    messageFactory.Create(
                        i.Status,
                        i.Channel,
                        i.DataByte1,
                        i.DataByte2
                    )
                );
            }
        }
    }
}