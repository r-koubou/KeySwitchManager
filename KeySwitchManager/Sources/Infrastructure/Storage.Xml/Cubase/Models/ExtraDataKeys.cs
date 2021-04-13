using KeySwitchManager.Domain.KeySwitches.Models.Values;

namespace KeySwitchManager.Infrastructure.Storage.Xml.Cubase.Models
{
    public static class ExtraDataKeys
    {
        public static readonly ExtraDataKey SlotName = new ExtraDataKey( "Cubase.Slot" );
        public static readonly ExtraDataKey ColorIndex = new ExtraDataKey( "Cubase.Color" );
        public static readonly ExtraDataKey GroupIndex = new ExtraDataKey( "Cubase.Group" );
        public static readonly ExtraDataKey ArticulationType = new ExtraDataKey( "Cubase.ArticulationType" );
    }
}