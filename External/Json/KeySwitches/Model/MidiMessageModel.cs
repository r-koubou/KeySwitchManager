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

        [JsonProperty( "data1")]
        [JsonRequired]
        public int Data1 { get; set; }

        [JsonProperty( "data2")]
        [JsonRequired]
        public int Data2 { get; set; }

        public MidiMessageModel()
        {}

        public MidiMessageModel(
            int status,
            int channel,
            int data1,
            int data2 )
        {
            Status    = status;
            Channel   = channel;
            Data1 = data1;
            Data2 = data2;
        }

    }
}