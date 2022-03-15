using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.StudioOne.Translators;

namespace KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.StudioOne
{
    public sealed class StudioOneWriter : IKeySwitchWriter
    {
        private Encoding FileEncoding { get; }
        private Stream? Stream { get; set; }

        public bool LeaveOpen { get; }

        public StudioOneWriter( Stream stream ) : this( stream, Encoding.UTF8 ) {}

        public StudioOneWriter( Stream stream, Encoding filEncoding, bool leaveOpen = false )
        {
            FileEncoding = filEncoding ?? throw new ArgumentNullException( nameof( filEncoding ) );
            Stream       = stream ?? throw new ArgumentNullException( nameof( stream ) );
            LeaveOpen    = leaveOpen;
        }

        public void Dispose()
        {
            if( LeaveOpen || Stream == null )
            {
                return;
            }

            Stream.Flush();
            Stream.Close();
            Stream.Dispose();
            Stream = null;
        }

        public void Write( IReadOnlyCollection<KeySwitch> keySwitches, IObserver<string>? loggingSubject = null )
        {
            if( Stream == null )
            {
                throw new NullReferenceException( nameof( Stream ) );
            }

            if( !keySwitches.Any() )
            {
                throw new ArgumentException( $"{nameof( keySwitches )} is empty" );
            }

            if( keySwitches.Count >= 2 )
            {
                throw new ArgumentException( $"{nameof( keySwitches )} has 1 element only" );
            }

            var source = keySwitches.First();

            loggingSubject?.OnNext( source.ToString() );

            using var writer = new StreamWriter( Stream, FileEncoding, IKeySwitchWriter.DefaultStreamWriterBufferSize, LeaveOpen );
            var xmlText = new StudioOneExportTranslator().Translate( source );

            writer.WriteLine( xmlText );
        }
    }
}
