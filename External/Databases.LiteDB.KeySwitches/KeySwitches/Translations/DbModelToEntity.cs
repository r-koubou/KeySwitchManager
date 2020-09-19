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
                var noteOn = new List<IMessage>();
                var controlChange = new List<IMessage>();
                var programChange = new List<IMessage>();

                ConvertMessageList( i.NoteOn,        noteOn,        new INoteOnFactory.Default() );
                ConvertMessageList( i.ControlChange, controlChange, new IControlChangeFactory.Default() );
                ConvertMessageList( i.ProgramChange, programChange, new IProgramChangeFactory.Default() );

                var articulation = new IArticulationFactory.Default().Create(
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

            return new IKeySwitchFactory.Default().Create(
                source.Id,
                source.Author,
                source.Description,
                source.Created,
                source.LastUpdated,
                source.DeveloperName,
                source.ProductName,
                source.InstrumentName,
                articulations
            );

        }

        private static void ConvertMessageList(
            IEnumerable<MidiMessageModel> src,
            ICollection<IMessage> dest,
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