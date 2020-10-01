using System.Collections.Generic;

using KeySwitchManager.Common.Text.Xml;
using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.UseCases.VstExpressionMap.Translations;
using KeySwitchManager.Xml.VstExpressionMap.Models;
using KeySwitchManager.Xml.VstExpressionMap.Models.XmlClasses;

namespace KeySwitchManager.Xml.VstExpressionMap.Translations
{
    public class KeySwitchToVstExpressionMapModel : IKeySwitchToVstExpressionMapModel
    {
        public IText Translate( KeySwitch source )
        {
            var slotTable = CollectSlotTable( source );

            #region List of USlotVisuals
            var listOfUSlotVisuals = new ListElement();

            foreach( var i in source.Articulations )
            {
                var type = ConvertArticulationType( i.ExtraData.GetValueOrDefault( ExtraDataKeys.ArticulationType, ExtraDataValue.Empty ) );
                var group = ConvertArticulationGroup( i.ExtraData.GetValueOrDefault( ExtraDataKeys.GroupIndex,      ExtraDataValue.Empty ) );

                var slotVisual = USlotVisuals.New(
                    i.ArticulationName.Value,
                    i.ArticulationName.Value,
                    0,
                    type,
                    group
                );
                listOfUSlotVisuals.Obj.Add( slotVisual );
            }
            #endregion List of USlotVisuals

            #region List of PSoundSlot
            var listOfPSoundSlot = new ListElement();

            foreach( var i in source.Articulations )
            {
                var slotName = i.ExtraData.GetValueOrDefault( ExtraDataKeys.SlotName, new ExtraDataValue( i.ArticulationName.Value ) );
                var slotColor = ConvertColorIndex( i.ExtraData.GetValueOrDefault( ExtraDataKeys.ColorIndex, ExtraDataValue.Empty ) );

                // PSoundSlot
                // PSoundSlot.name
                var pSoundSlot = PSoundSlot.New( slotName.Value );

                // PSoundSlot.PSlotThruTrigger
                pSoundSlot.Obj.Add( PSlotThruTrigger.New() );

                // PSoundSlot -> PSlotMidiAction -> POutputEvent
                var listOfPOutputEvent = new ListElement();
                ConvertOutputMappings( i, listOfPOutputEvent );

                // PSoundSlot -> PSlotMidiAction
                pSoundSlot.Obj.Add( PSlotMidiAction.New( listOfPOutputEvent ) );

                // PSoundSlot.sv
                var slotVisualList = new List<ObjectElement>();

                foreach( var articulation in slotTable[ slotName.Value ] )
                {
                    var type = ConvertArticulationType( articulation.ExtraData.GetValueOrDefault( ExtraDataKeys.ArticulationType, ExtraDataValue.Empty ) );
                    var group = ConvertArticulationGroup( articulation.ExtraData.GetValueOrDefault( ExtraDataKeys.GroupIndex,     ExtraDataValue.Empty ) );

                    slotVisualList.Add(
                        USlotVisuals.New(
                            articulation.ArticulationName.Value,
                            articulation.ArticulationName.Value,
                            0,
                            type,
                            group )
                    );
                }

                pSoundSlot.Member.Add( PSoundSlot.Sv( slotVisualList ) );

                // PSoundSlot.color
                pSoundSlot.Int.Add( new IntElement( "color", slotColor ) );

                // Aggregate
                listOfPSoundSlot.Obj.Add( pSoundSlot );

            }
            #endregion List of PSoundSlot

            #region Create a RootElement
            // Construction of InstrumentMap element
            var slots = InstrumentMap.Slots( listOfPSoundSlot );
            var slotVisuals = InstrumentMap.SlotVisuals( listOfUSlotVisuals );

            var instrumentName = $"{source.ProductName.Value} {source.InstrumentName.Value}";
            var rootElement = InstrumentMap.New( instrumentName );
            rootElement.Member.Add( slotVisuals );
            rootElement.Member.Add( slots );
            rootElement.StringElement.Value = instrumentName;
            #endregion

            return new PlainText( XmlHelper.ToXmlString( rootElement ) );
        }

        private static IReadOnlyDictionary<string, ICollection<Articulation>> CollectSlotTable( KeySwitch keySwitch )
        {
            var result = new Dictionary<string, ICollection<Articulation>>();

            foreach( var articulation in keySwitch.Articulations )
            {
                var slotName = articulation.ExtraData.GetValueOrDefault(
                    ExtraDataKeys.SlotName,
                    new ExtraDataValue( articulation.ArticulationName.Value )
                ).Value;

                if( !result.ContainsKey( slotName ) )
                {
                    result[ slotName ] = new List<Articulation>();
                }
                result[ slotName ].Add( articulation );
            }

            return result;
        }

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
            ConvertOutputMappingsImpl( articulation.MidiNoteOns, listOfPOutputEvent );
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

    }
}