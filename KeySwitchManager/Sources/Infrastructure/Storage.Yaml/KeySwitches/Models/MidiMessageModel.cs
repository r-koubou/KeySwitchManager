using System.ComponentModel.DataAnnotations;

namespace KeySwitchManager.Storage.Yaml.KeySwitches.Models
{
    public class MidiMessageModel
    {
        [Required]
        public int Status { get; set; }

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
            Data1   = data1;
            Data2   = data2;
        }

    }
}