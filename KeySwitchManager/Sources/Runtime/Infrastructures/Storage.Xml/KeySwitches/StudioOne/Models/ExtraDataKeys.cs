using KeySwitchManager.Domain.KeySwitches.Models.Values;

namespace KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.StudioOne.Models
{
    public static class ExtraDataKeys
    {
        public static readonly string ValueSeparator = ",";

        public static readonly ExtraDataKey Color = new ExtraDataKey( "StudioOne.Color" );
        public static readonly ExtraDataKey Momentary = new ExtraDataKey( "StudioOne.Momentary" );

        public static readonly ExtraDataKey NoteOnOff = new ExtraDataKey( "StudioOne.NoteOnOff" );
        public static readonly ExtraDataKey NoteOn = new ExtraDataKey( "StudioOne.NoteOn" );
        public static readonly ExtraDataKey NoteOff = new ExtraDataKey( "StudioOne.NoteOff" );

        public static readonly ExtraDataKey Bank = new ExtraDataKey( "StudioOne.Bank" );
    }
}