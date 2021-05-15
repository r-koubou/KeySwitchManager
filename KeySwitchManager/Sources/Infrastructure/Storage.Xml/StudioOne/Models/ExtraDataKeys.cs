using KeySwitchManager.Domain.KeySwitches.Models.Values;

namespace KeySwitchManager.Infrastructure.Storage.Xml.StudioOne.Models
{
    public static class ExtraDataKeys
    {
        public static readonly ExtraDataKey Color = new ExtraDataKey( "StudioOne.Color" );
        public static readonly ExtraDataKey Momentary = new ExtraDataKey( "StudioOne.Momentary" );
    }
}