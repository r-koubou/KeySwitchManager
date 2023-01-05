using System.Collections.Generic;

using Claunia.PropertyList;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Aggregations;
using KeySwitchManager.Domain.MidiMessages.Models.Aggregations;
using KeySwitchManager.Infrastructures.Storage.Plist.KeySwitches.Logic.Models.Aggregations;

namespace KeySwitchManager.Infrastructures.Storage.Plist.KeySwitches.Logic.Translators.Helpers
{
    internal static class TranslateModelHelper
    {
        public static NSDictionary Translate( KeySwitch source )
        {
            var result = new NSDictionary();
            int id = 1001;
            int articulationId = 1;

            #region Articulations
            {
                var articulations = new NSArray();
                foreach( var articulation in source.Articulations )
                {
                    articulations.Add( TranslateArticulation( articulation, id, articulationId ) );
                    id++;
                    articulationId++;
                }
                result.Add( "Articulations", articulations );
            }
            #endregion

            result.Add( "Name", $"{source.InstrumentName.Value}.plist" );


            return result;
        }

        public static NSDictionary TranslateArticulation( Articulation articulation, int id, int articulationId )
        {
            var outputDictionary = new NSDictionary();

            ConvertChannelVoiceMessageList( articulation.MidiNoteOns,        outputDictionary );
            ConvertChannelVoiceMessageList( articulation.MidiControlChanges, outputDictionary );
            ConvertChannelVoiceMessageList( articulation.MidiProgramChanges, outputDictionary );

            var outputArray = new NSArray(outputDictionary);

            var result = new NSDictionary();
            result.Add( "ArticulationID", articulationId );
            result.Add( "ID",             id );
            result.Add( "Name",           articulation.ArticulationName.Value );
            result.Add( "Output",         outputArray );

            return result;
        }


        #region Converting
        private static void ConvertChannelVoiceMessageList(
            IEnumerable<IMidiMessage> src,
            NSDictionary dest )
        {
            foreach( var i in src )
            {
                var data1 = i.DataByte1.Value;
                var data2 = i.DataByte2.Value;

                dest.Add( "MB1",    data1 );

                if( i is MidiNoteOn )
                {
                    dest.Add( "Status", "Note On" );
                }
                else if( i is MidiControlChange )
                {
                    dest.Add( "Status", "Controller" );
                }
                else if( i is MidiProgramChange )
                {
                    dest.Add( "Status", "Program" );
                }

                dest.Add( "ValueLow", data2 );
            }
        }
        #endregion


        // public static RootModel Translate( KeySwitch source )
        // {
        //     int id = 1001;
        //     int articulationId = 1;
        //
        //     var result = new RootModel();
        //
        //     foreach( var i in source.Articulations )
        //     {
        //         result.Articulations.Add( TranslateArticulation( i, id, articulationId ) );
        //     }
        //
        //     return result;
        // }
        //
        // public static ArticulationModel TranslateArticulation( Articulation articulation, int id, int articulationId )
        // {
        //     var result = new ArticulationModel
        //     {
        //         Id = id,
        //         ArticulationId = articulationId,
        //         Name = articulation.ArticulationName.Value,
        //         Output = TranslateOutput( articulation )
        //     };
        //
        //     return result;
        // }
        //
        // public static OutputModel TranslateOutput( Articulation articulation )
        // {
        //     var noteOn = new List<MidiNoteOnModel>();
        //     var controlChange = new List<MidiControlChangeModel>();
        //     var programChange = new List<MidiProgramChangeModel>();
        //
        //     ConvertChannelVoiceMessageList( articulation.MidiNoteOns,        noteOn,        IMidiNoteOnModelFactory.Default );
        //     ConvertChannelVoiceMessageList( articulation.MidiControlChanges, controlChange, IMidiControlChangeModelFactory.Default );
        //     ConvertChannelVoiceMessageList( articulation.MidiProgramChanges, programChange, IMidiProgramChangeModelFactory.Default );
        //
        //     var result = new OutputModel();
        //     result.MidiMessageModels.AddRange( noteOn );
        //     result.MidiMessageModels.AddRange( controlChange );
        //     result.MidiMessageModels.AddRange( programChange );
        //
        //     return result;
        // }
        //
        //
        // #region Converting
        // private static void ConvertChannelVoiceMessageList<T>(
        //     IEnumerable<IMidiMessage> src,
        //     ICollection<T> dest,
        //     IMidiChannelVoiceMessageModelFactory<T> factory ) where T : IMidiChannelVoiceMessageModel
        // {
        //     foreach( var i in src )
        //     {
        //         dest.Add(
        //             factory.Create(
        //                 i.Status.Value & 0xF,
        //                 i.DataByte1.Value,
        //                 i.DataByte2.Value
        //             )
        //         );
        //     }
        // }
        // #endregion
    }
}
