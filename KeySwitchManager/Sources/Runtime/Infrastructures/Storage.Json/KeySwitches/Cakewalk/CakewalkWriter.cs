using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Json.KeySwitches.Cakewalk.Translators;

namespace KeySwitchManager.Infrastructures.Storage.Json.KeySwitches.Cakewalk
{
    public sealed class CakewalkWriter : IKeySwitchWriter
    {
        private Encoding FileEncoding { get; }
        private Stream? Stream { get; set; }

        public bool LeaveOpen { get; }

        public CakewalkWriter( Stream stream ) : this( stream, Encoding.UTF8 ) {}

        public CakewalkWriter( Stream stream, Encoding filEncoding, bool leaveOpen = false )
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

            using var writer = new StreamWriter( Stream, FileEncoding );
            // TODO すべての要素を束ねた 1 JSONファイルにしたい(Cakewalkは保持できる)
            var jsonText = new CakewalkExportTranslator( true ).Translate( source );

            writer.WriteLine( jsonText );
        }
    }
}
