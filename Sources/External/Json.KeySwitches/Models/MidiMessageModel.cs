using KeySwitchManager.Domain.MidiMessages.Helpers;

using Newtonsoft.Json;

namespace KeySwitchManager.Json.KeySwitches.Models
{
    [JsonObject("midi_message")]
    public class MidiMessageModel
    {
        [JsonProperty( "status" )]
        [JsonRequired]
        public int Status { get; set; }

        [JsonProperty( "channel" )]
        public int Channel { get; set; }

        [JsonProperty( "data1" )]
        public int Data1 { get; set; }

        [JsonProperty( "data2" )]
        public int Data2 { get; set; }

        public MidiMessageModel()
        {}

        public MidiMessageModel(
            int status,
            int data1,
            int data2 )
        {
            Status  = status;
            Channel = MidiStatusHelper.GetChannel( status );
            Data1   = data1;
            Data2   = data2;
        }

    }
}