using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Aggregations;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Domain.MidiMessages.Models.Aggregations;
using KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.Cubase.Models;
using KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.Cubase.Models.XmlClasses;

namespace KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.Cubase.Translators.Helpers
{
    internal static class TranslateModelHelper
    {
        public static RootElement TranslateRootElement( KeySwitch source )
        {
            var slotTable = CollectSlotTable( source );

            var listOfUSlotVisuals = ConvertUSlotVisualsList( source );
            var listOfPSoundSlot = ConvertPSoundSlotList( slotTable );

            var rootElement = ConvertRootElement(
                source,
                listOfPSoundSlot,
                listOfUSlotVisuals
            );

            return rootElement;
        }

        #region Convert RootElement
        private static RootElement ConvertRootElement( KeySwitch source, ListElement listOfPSoundSlot, ListElement listOfUSlotVisuals )
        {
            // Construction of InstrumentMap element
            var slots = InstrumentMap.Slots( listOfPSoundSlot );
            var slotVisuals = InstrumentMap.SlotVisuals( listOfUSlotVisuals );

            var instrumentName = $"{source.ProductName.Value} {source.InstrumentName.Value}";
            var rootElement = InstrumentMap.New( instrumentName );
            rootElement.Member.Add( slotVisuals );
            rootElement.Member.Add( slots );
            rootElement.StringElement.Value = instrumentName;

            return rootElement;
        }
        #endregion Convert RootElement

        #region Convert To USlotVisual List

        private static ListElement ConvertUSlotVisualsList( KeySwitch source )
        {
            var listOfUSlotVisuals = new ListElement();

            foreach( var i in source.Articulations )
            {
                var type = ConvertArticulationType( i.ExtraData.GetValueOrDefault( ExtraDataKeys.ArticulationType, ExtraDataValue.Empty ) );
                var group = ConvertArticulationGroup( i.ExtraData.GetValueOrDefault( ExtraDataKeys.GroupIndex,     ExtraDataValue.Empty ) );

                var slotVisual = USlotVisuals.New(
                    i.ArticulationName.Value,
                    i.ArticulationName.Value,
                    0,
                    type,
                    @group
                );
                listOfUSlotVisuals.Obj.Add( slotVisual );
            }

            return listOfUSlotVisuals;
        }

        #endregion

        #region Convert To PSoundSlot List

        private static ListElement ConvertPSoundSlotList( IReadOnlyDictionary<string, ICollection<Articulation>> slotTable )
        {
            var listOfPSoundSlot = new ListElement();

            foreach( var pair in slotTable )
            {
                var slotName = pair.Key;

                // PSoundSlot
                // PSoundSlot.name
                var pSoundSlot = ConvertPSoundSlot( slotName );

                // PSoundSlot -> PSlotMidiAction -> POutputEvent
                var listOfPOutputEvent = ConvertPOutputEventList( pair.Value );

                // PSoundSlot -> PSlotMidiAction
                pSoundSlot.Obj.Add( PSlotMidiAction.New( listOfPOutputEvent ) );

                // PSoundSlot.sv
                var slotVisualList = ConvertSlotVisualList( pair.Value );
                pSoundSlot.Member.Add( PSoundSlot.Sv( slotVisualList ) );

                // PSoundSlot.color
                // TODO アーティキュレーション変数にアクセスできないため、 ExtraData の Color を参照できず
                //pSoundSlot.Int.Add( new IntElement( "color", ConvertColorIndex( ....[ExtraDataKeys.Color] ) ) );
                pSoundSlot.Int.Add( new IntElement( "color", 0 ) );

                // Aggregate
                listOfPSoundSlot.Obj.Add( pSoundSlot );
            }

            return listOfPSoundSlot;
        }

        private static ObjectElement ConvertPSoundSlot( string slotName )
        {
            // PSoundSlot
            // PSoundSlot.name
            var pSoundSlot = PSoundSlot.New( slotName );

            // PSoundSlot.PSlotThruTrigger
            pSoundSlot.Obj.Add( PSlotThruTrigger.New() );

            return pSoundSlot;
        }

        private static ListElement ConvertPOutputEventList( IEnumerable<Articulation> articulations )
        {
            // PSoundSlot -> PSlotMidiAction -> POutputEvent
            var listOfPOutputEvent = new ListElement();

            foreach( var i in articulations )
            {
                ConvertOutputMappings( i, listOfPOutputEvent );
            }

            return listOfPOutputEvent;
        }

        private static List<ObjectElement> ConvertSlotVisualList( IEnumerable<Articulation> articulations )
        {
            // PSoundSlot.sv
            var slotVisualList = new List<ObjectElement>();

            foreach( var articulation in articulations )
            {
                var type = ConvertArticulationType( articulation.ExtraData.GetValueOrDefault( ExtraDataKeys.ArticulationType, ExtraDataValue.Empty ) );
                var group = ConvertArticulationGroup( articulation.ExtraData.GetValueOrDefault( ExtraDataKeys.GroupIndex,     ExtraDataValue.Empty ) );

                slotVisualList.Add(
                    USlotVisuals.New(
                        articulation.ArticulationName.Value,
                        articulation.ArticulationName.Value,
                        0,
                        type,
                        @group
                    )
                );
            }

            return slotVisualList;
        }

        private static IReadOnlyDictionary<string, ICollection<Articulation>> CollectSlotTable( KeySwitch keySwitch )
        {
            static void AddArticulation( IDictionary<string, ICollection<Articulation>> dictionary, string key, Articulation articulation )
            {
                if( !dictionary.ContainsKey( key ) )
                {
                    dictionary[ key ] = new List<Articulation>();
                }

                dictionary[ key ].Add( articulation );
            }

            var result = new Dictionary<string, ICollection<Articulation>>();

            foreach( var articulation in keySwitch.Articulations )
            {
                // use an articulation name as slot name if user does not define a slot name
                if( !articulation.ExtraData.TryGetValue( ExtraDataKeys.SlotName, out var extraValue ) )
                {
                    AddArticulation( result, articulation.ArticulationName.Value, articulation );
                    continue;
                }

                // User can assign articulation to any slot by comma ( , ) separated
                var slotNames = extraValue.Value.Split( ',' );
                foreach( var slotName in slotNames )
                {
                    var key = slotName.Trim();
                    AddArticulation( result, key, articulation );
                }
            }

            return result;
        }

        #endregion Convert To PSoundSlot List

        #region Sub Routines
        private static int ConvertArticulationType( ExtraDataValue value )
        {
            return value.Value switch
            {
                "Attribute" => 0,
                "Direction" => 1,
                _           => 1
            };
        }

        private static int ConvertArticulationGroup( ExtraDataValue value )
        {
            return int.TryParse( value.Value, out var result ) ? result : 0;
        }

        private static int ConvertColorIndex( ExtraDataValue value )
        {
            return int.TryParse( value.Value, out var result ) ? result : 0;
        }

        private static void ConvertOutputMappings( Articulation articulation, ListElement listOfPOutputEvent )
        {
            ConvertOutputMappingsImpl( articulation.MidiNoteOns,        listOfPOutputEvent );
            ConvertOutputMappingsImpl( articulation.MidiControlChanges, listOfPOutputEvent );
            ConvertOutputMappingsImpl( articulation.MidiProgramChanges, listOfPOutputEvent );
        }

        private static void ConvertOutputMappingsImpl( IEnumerable<IMidiMessage> midiEventList, ListElement listOfPOutputEvent )
        {
            foreach( var i in midiEventList )
            {
                var status = i.Status.Value;
                var data1 = i.DataByte1.Value;
                var data2 =  i.DataByte2.Value;
                listOfPOutputEvent.Obj.Add( POutputEvent.New( status, data1, data2 ) );
            }
        }
        #endregion

    }
}