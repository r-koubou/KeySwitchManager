using System.Collections.Generic;

using Claunia.PropertyList;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Aggregations;
using KeySwitchManager.Domain.MidiMessages.Models.Aggregations;

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

            var outputArray = new NSArray();
            ConvertChannelVoiceMessageList( articulation.MidiNoteOns,        outputArray );
            ConvertChannelVoiceMessageList( articulation.MidiControlChanges, outputArray );
            ConvertChannelVoiceMessageList( articulation.MidiProgramChanges, outputArray );

            var result = new NSDictionary();
            result.Add( "ArticulationID", articulationId );
            result.Add( "ID",             id );
            result.Add( "Name",           articulation.ArticulationName.Value );
            result.Add( "Output",         outputArray );

            return result;
        }


        #region Converting
        private static void ConvertChannelVoiceMessageList( IEnumerable<IMidiMessage> src, NSArray dest )
        {
            foreach( var i in src )
            {
                var midiMessageDictionary = new NSDictionary();

                var data1 = i.DataByte1.Value;
                var data2 = i.DataByte2.Value;

                midiMessageDictionary.Add( "MB1",    data1 );

                if( i is MidiNoteOn )
                {
                    midiMessageDictionary.Add( "Status", "Note On" );
                }
                else if( i is MidiControlChange )
                {
                    midiMessageDictionary.Add( "Status", "Controller" );
                }
                else if( i is MidiProgramChange )
                {
                    midiMessageDictionary.Add( "Status", "Program" );
                }

                midiMessageDictionary.Add( "ValueLow", data2 );

                dest.Add( midiMessageDictionary );
            }
        }
        #endregion
    }
}
