using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Value;

namespace KeySwitchManager.Domain.KeySwitches
{
    public interface IExtraDataFactory
    {
        public ExtraData Create( IReadOnlyDictionary<string, string> source );
        public IReadOnlyDictionary<string, string> Create( ExtraData source );

        public static IExtraDataFactory Default => new DefaultFactory();

        private class DefaultFactory : IExtraDataFactory
        {
            public ExtraData Create( IReadOnlyDictionary<string, string> source )
            {
                var result = new Dictionary<ExtraDataKey, ExtraDataValue>();

                foreach( var key in source.Keys )
                {
                    var k = new ExtraDataKey( key );
                    var v = new ExtraDataValue( source[ key ] );
                    result.Add( k, v );
                }

                return new ExtraData( result );
            }

            public IReadOnlyDictionary<string, string> Create( ExtraData source )
            {
                var result = new Dictionary<string, string>();

                foreach( var key in source.Keys )
                {
                    result.Add( key.Value, source[ key ].Value );
                }

                return result;
            }

        }
    }
}