using System.Collections.Generic;

using KeySwitchManager.Common.Text.Xml;
using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
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
            #region List of USlotVisuals
            var listOfUSlotVisuals = new ListElement();

            foreach( var i in source.Articulations )
            {
                var slotVisual = USlotVisuals.New(
                    i.ArticulationName.Value,
                    i.ArticulationName.Value,
                    0,
                    (int)i.ArticulationType,
                    i.ArticulationGroup.Value
                );
                listOfUSlotVisuals.Obj.Add( slotVisual );
            }
            #endregion List of USlotVisuals

            #region List of PSoundSlot
            var listOfPSoundSlot = new ListElement();

            foreach( var i in source.Articulations )
            {
                var slotName = i.ArticulationName.Value;
                var slotColor = i.ArticulationColor.Value;

                // PSoundSlot
                var pSoundSlot = PSoundSlot.New( slotName );
                pSoundSlot.Obj.Add( PSlotThruTrigger.New() );

                // POutputEvent
                var listOfPOutputEvent = new ListElement();
                ConvertOutputMappings( i, listOfPOutputEvent );

                // slotMidiAction
                pSoundSlot.Obj.Add( PSlotMidiAction.New( listOfPOutputEvent ) );

                // sv
                var slotVisual = USlotVisuals.New(
                    i.ArticulationName.Value,
                    i.ArticulationName.Value,
                    0,
                    (int)i.ArticulationType,
                    i.ArticulationGroup.Value
                );

                pSoundSlot.Member.Add( PSoundSlot.Sv( slotVisual ) );

                // name
                pSoundSlot.Member.Add( PSoundSlot.Name( slotName ) );

                //
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