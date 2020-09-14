namespace ArticulationManager.Databases.LiteDB.KeySwitches.Model
{
    public class MidiMessageModel
    {
        public int Status { get; set; }
        public int Channel { get; set; }
        public int DataByte1 { get; set; }
        public int DataByte2 { get; set; }

        public MidiMessageModel()
        {}

        public MidiMessageModel(
            int status,
            int channel,
            int dataByte1,
            int dataByte2 )
        {
            Status    = status;
            Channel   = channel;
            DataByte1 = dataByte1;
            DataByte2 = dataByte2;
        }

    }
}