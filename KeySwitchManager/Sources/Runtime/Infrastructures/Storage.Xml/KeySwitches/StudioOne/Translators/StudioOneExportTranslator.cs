using System.Collections.Generic;
using System.Linq;
using System.Text;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Aggregations;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Domain.KeySwitches.Models.Values.Extensions;
using KeySwitchManager.Domain.MidiMessages.Models.Values;
using KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.StudioOne.Models;

using RkHelper.Text.Xml;

namespace KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.StudioOne.Translators
{
    public class StudioOneExportTranslator : IDataTranslator<KeySwitch, IText>
    {
        public IText Translate( KeySwitch source )
        {
            var rootElement = TranslateRootElement( source );
            var attributeElements = TranslateElementAttributes( source.Articulations );

            rootElement.AttributeElements.AddRange( attributeElements );

            return new PlainText( XmlHelper.ToXmlString( rootElement ) );

        }

        #region Translate Attribute
        public static RootElement TranslateRootElement( KeySwitch source )
            => new()
            {
                Name = $"{source.ProductName.Value} {source.InstrumentName}"
            };

        public static RootElement TranslateRootElement( DeveloperName developerName, ProductName productName )
            => new()
            {
                Name = $"{developerName.Value} {productName.Value}"
            };

        public static RootElement TranslateRootElement( ProductName productName, InstrumentName instrumentName )
            => new()
            {
                Name = $"{productName.Value} {instrumentName.Value}"
            };

        public static RootElement TranslateRootElement( ProductName productName )
            => new()
            {
                Name = $"{productName.Value}"
            };

        public static IReadOnlyCollection<AttributeElement> TranslateElementAttributes( IEnumerable<Articulation> articulations )
        {
            var result = new List<AttributeElement>();
            var id = 0;

            foreach( var i in articulations )
            {
                if( !i.MidiNoteOns.Any() )
                {
                    continue;
                }

                var attr = TranslateElementAttribute( i, id );
                id++;

                result.Add( attr );
            }

            return result;
        }

        private static AttributeElement TranslateElementAttribute( Articulation articulation, int id )
        {
            var name = articulation.ArticulationName.Value;
            var pitch = articulation.MidiNoteOns[ 0 ].DataByte1.Value;
            var activation = TranslateActivation( articulation );

            string color = default!;

            if( articulation.ExtraData.ContainsKey( ExtraDataKeys.Color ) )
            {
                color = articulation.ExtraData[ ExtraDataKeys.Color ].Value;
            }

            var momentary = articulation.ExtraData.GetValueOrDefault(
                ExtraDataKeys.Momentary, new ExtraDataValue( "0" )
            ).Value == "0" ? 0 : 1;

            return new AttributeElement( name, id, color, pitch, momentary, activation );
        }

        #region Translate Activations
        private static string TranslateActivation( Articulation articulation )
        {
            var activations = new List<string>();
            var sb = new StringBuilder( 128 );

            activations.AddRange(  TranslateActivationNote( articulation ) );
            activations.AddRange( TranslateActivationControlChange( articulation ) );
            activations.AddRange( TranslateActivationProgramChange( articulation ) );
            activations.AddRange( TranslateActivationBankChange( articulation ) );

            activations = activations.Distinct().ToList();

            var count = activations.Count;
            for( var i = 0; i < count; i++ )
            {
                var x = activations[ i ];
                sb.Append( x );

                if( i < count - 1 )
                {
                    sb.Append( '|' );
                }
            }

            return sb.ToString();
        }

        private static IReadOnlyList<string> TranslateActivationNote( Articulation articulation )
        {
            var result = new List<string>();

            foreach( var x in articulation.MidiNoteOns )
            {
                var byte1 = x.DataByte1.Value;
                var byte2 = x.DataByte2.Value;
                result.Add( $"note{byte1}.{byte2}" );
            }

            #region Convert from Extra keys
            void TranslateExtraData( string prefix, ExtraDataKey k )
            {

                articulation.ExtraData.KeyWithIndexCount( k, ( _, v, _ ) =>
                {
                    var values = v.Value.Split( ExtraDataKeys.ValueSeparator );
                    var note = new MidiNoteName( values[ 0 ].Trim() ).ToMidiNoteNumber().Value;
                    var velocity = int.Parse( values[ 1 ].Trim() );
                    result.Add( $"{prefix}{note}.{velocity}" );

                });
            }

            TranslateExtraData( "note", ExtraDataKeys.NoteOnOff );
            TranslateExtraData( "on", ExtraDataKeys.NoteOn );
            TranslateExtraData( "off", ExtraDataKeys.NoteOff );
            #endregion

            return result;
        }

        private static IReadOnlyCollection<string> TranslateActivationControlChange( Articulation articulation )
        {
            var result = new List<string>();

            foreach( var x in articulation.MidiControlChanges )
            {
                var ccNo = x.Status.Value;
                var byte1 = x.DataByte1.Value;
                var byte2 = x.DataByte2.Value;

                if( ccNo is 0 or 32 )
                {
                    result.Add($"bc{byte1}.{byte2}" );
                }
                else
                {
                    result.Add( $"cc{byte1}.{byte2}" );
                }

            }

            return result;
        }

        private static IReadOnlyCollection<string> TranslateActivationProgramChange( Articulation articulation )
        {
            var result = new List<string>();

            foreach( var x in articulation.MidiProgramChanges )
            {
                var byte1 = x.DataByte1.Value;
                result.Add( $"pc{byte1}" );
            }

            return result;
        }

        private static IReadOnlyCollection<string> TranslateActivationBankChange( Articulation articulation )
        {
            var result = new List<string>();

            #region Convert from Extra keys

            articulation.ExtraData.KeyWithIndexCount( ExtraDataKeys.Bank, ( _, v, _ ) =>
            {
                var values = v.Value.Split( ExtraDataKeys.ValueSeparator );
                var data1 = int.Parse( values[ 0 ].Trim() );
                var data2 = int.Parse( values[ 1 ].Trim() );
                result.Add( $"bc{data1}.{data2}" );
            });
            #endregion

            return result;
        }
        #endregion
        #endregion

    }
}