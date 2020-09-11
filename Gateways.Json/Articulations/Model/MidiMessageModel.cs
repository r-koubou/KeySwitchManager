using Newtonsoft.Json;

namespace ArticulationManager.Gateways.Json.Articulations.Model
{
    [JsonObject("midi_message")]
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
            int dataByte1,
            int dataByte2 )
            : this( status, 0x00, dataByte1, dataByte2 )
        {
        }

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