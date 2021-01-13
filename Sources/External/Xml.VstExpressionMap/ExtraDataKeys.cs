using KeySwitchManager.Domain.KeySwitches.Value;

namespace KeySwitchManager.Xml.VstExpressionMap
{
    public static class ExtraDataKeys
    {
        public static readonly ExtraDataKey SlotName = IExtraDataKeyFactory.Default.Create( "Cubase.Slot" );
        public static readonly ExtraDataKey ColorIndex = IExtraDataKeyFactory.Default.Create( "Cubase.Color" );
        public static readonly ExtraDataKey GroupIndex = IExtraDataKeyFactory.Default.Create( "Cubase.Group" );
        public static readonly ExtraDataKey ArticulationType = IExtraDataKeyFactory.Default.Create( "Cubase.ArticulationType" );
    }
}