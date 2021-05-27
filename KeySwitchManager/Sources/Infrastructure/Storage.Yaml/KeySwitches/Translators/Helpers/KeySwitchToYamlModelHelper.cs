using System.Collections.Generic;

using KeySwitchManager.Commons.Helpers;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Domain.MidiMessages.Models.Aggregations;
using KeySwitchManager.Storage.Yaml.KeySwitches.Models;
using KeySwitchManager.Storage.Yaml.KeySwitches.Models.Entities;

namespace KeySwitchManager.Storage.Yaml.KeySwitches.Translators.Helpers
{
    internal static class KeySwitchToYamlModelHelper
    {
        public static KeySwitchModel Translate( KeySwitch source )
        {
            var articulationModels = new List<ArticulationModel>();

            foreach( var i in source.Articulations )
            {
                var noteOn = new List<MidiNoteOnModel>();
                var controlChange = new List<MidiControlChangeModel>();
                var programChange = new List<MidiProgramChangeModel>();

                ConvertChannelVoiceMessageList( i.MidiNoteOns,        noteOn,        IMidiNoteOnModelFactory.Default );
                ConvertChannelVoiceMessageList( i.MidiControlChanges, controlChange, IMidiControlChangeModelFactory.Default );
                ConvertChannelVoiceMessageList( i.MidiProgramChanges, programChange, IMidiProgramChangeModelFactory.Default );

                var yamlObject = new ArticulationModel(
                    i.ArticulationName.Value,
                    new MidiModel(
                        noteOn,
                        controlChange,
                        programChange
                    ),
                    ConvertExtraData( i.ExtraData )
                );

                articulationModels.Add( yamlObject );
            }

            return new KeySwitchModel(
                source.Id.Value,
                source.Author.Value,
                source.Description.Value,
                UtcDateTimeHelper.ToDateTime( source.Created ),
                UtcDateTimeHelper.ToDateTime( source.LastUpdated ),
                source.DeveloperName.Value,
                source.ProductName.Value,
                source.InstrumentName.Value,
                articulationModels,
                IExtraDataFactory.Default.Create( source.ExtraData )
            );
        }

        #region Converting
        private static void ConvertChannelVoiceMessageList<T>(
            IEnumerable<IMidiMessage> src,
            ICollection<T> dest,
            IMidiChannelVoiceMessageModelFactory<T> factory ) where T : IMidiChannelVoiceMessageModel
        {
            foreach( var i in src )
            {
                dest.Add(
                    factory.Create(
                        i.Status.Value & 0xF,
                        i.DataByte1.Value,
                        i.DataByte2.Value
                    )
                );
            }
        }

        private static void ConvertMessageList(
            IEnumerable<IMidiMessage> src,
            ICollection<IMidiMessageModel> dest,
            IMidiMessageModelFactory<IMidiMessageModel> factory )
        {
            foreach( var i in src )
            {
                dest.Add(
                    factory.Create(
                        i.Status.Value,
                        i.DataByte1.Value,
                        i.DataByte2.Value
                    )
                );
            }
        }

        private static Dictionary<string, string> ConvertExtraData( ExtraData source )
        {
            var extra = new Dictionary<string, string>();

            foreach( var key in source.Keys )
            {
                var k = key.Value;
                var v = source[ key ].Value;
                extra.Add( key.Value, v );
            }

            return extra;
        }
        #endregion

    }
}