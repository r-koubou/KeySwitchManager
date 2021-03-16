using System.ComponentModel.DataAnnotations;

using KeySwitchManager.Domain.MidiMessages.Helper;

namespace KeySwitchManager.Json.KeySwitch.Model
{
    public class MidiMessageModel
    {
        [Required]
        public int Status { get; set; }

        public int Channel { get; set; }

        public int Data1 { get; set; }

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