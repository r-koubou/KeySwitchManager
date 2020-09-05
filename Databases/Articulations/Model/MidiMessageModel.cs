namespace ArticulationManager.Databases.Articulations.Model
{
    public class MidiMessageModel
    {
        public int Status { get; set; }
        public int DataByte1 { get; set; }
        public int DataByte2 { get; set; }

        public MidiMessageModel()
        {}

        public MidiMessageModel(
            int status,
            int dataByte1,
            int dataByte2 )
        {
            Status            = status;
            DataByte1         = dataByte1;
            DataByte2         = dataByte2;
        }

    }
}