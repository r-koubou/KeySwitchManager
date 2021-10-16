using System;

namespace KeySwitchManager.Domain.KeySwitches.Models.Values.Extensions
{
    public static class ExtraDataExtension
    {

        public static void KeyWithIndexCount( this ExtraData extraData, ExtraDataKey prefix, Action<ExtraDataKey, ExtraDataValue, int> notify, int startIndex = 1, int endIndex = int.MaxValue )
        {
            for( var i = startIndex; i < endIndex; i++ )
            {
                var key = new ExtraDataKey( $"{prefix.Value}{i}" );

                if( !extraData.ContainsKey( key ) )
                {
                    break;
                }

                notify.Invoke( key, extraData[ key ], i );
            }
        }
    }
}
