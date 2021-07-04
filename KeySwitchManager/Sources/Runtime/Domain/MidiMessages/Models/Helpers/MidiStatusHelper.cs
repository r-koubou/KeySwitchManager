namespace KeySwitchManager.Domain.MidiMessages.Models.Helpers
{
    public static class MidiStatusHelper
    {
        #region Presets
        public const int NoteOn = 0x90;
        public const int ControlChange = 0xB0;
        public const int ProgramChange = 0xC0;
        #endregion

        public static int MakeStatus( int status, int channel )
        {
            return status | ( channel & 0x0F );
        }

        public static int GetChannel( int status )
        {
            return status & 0x0F;
        }

    }
}