using System.Collections.Generic;

using ArticulationManager.Common.Utilities;
using ArticulationManager.Databases.LiteDB.KeySwitches.Model;
using ArticulationManager.Domain.KeySwitches;
using ArticulationManager.Domain.KeySwitches.Aggregate;
using ArticulationManager.Domain.KeySwitches.Value;
using ArticulationManager.Domain.MidiMessages;
using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.Translations;

namespace ArticulationManager.Databases.LiteDB.KeySwitches.Translations
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