using Newtonsoft.Json;

namespace KeySwitchManager.Json.KeySwitches.Model
{
    [JsonObject("midi_message")]
    public class MidiMessageModel
    {
        [JsonProperty( "status")]
        [JsonRequired]
        public int Status { get; set; }
        [JsonProperty( "channel")]
        [JsonRequired]
        public int Channel { get; set; }
        [JsonProperty( "data_byte_1")]
        [JsonRequired]
        public int DataByte1 { get; set; }
        [JsonProperty( "data_byte_2")]
        [JsonRequired]
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