using System;

using ArticulationManager.Common.Utilities;
using ArticulationManager.Domain.Articulations.Value;

using Newtonsoft.Json;

namespace ArticulationManager.Json.Articulations.Model
{
    [JsonObject("articulation")]
    public class ArticulationModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty( "created" )]
        public DateTime Created { get; set; } = DateTimeHelper.NowUtc();
        [JsonProperty( "last_updated")]
        public DateTime LastUpdated { get; set; } = DateTimeHelper.NowUtc();
        [JsonProperty( "developer_name")]
        public string DeveloperName { get; set; } = default!;
        [JsonProperty( "product_name")]
        public string ProductName { get; set; } = default!;
        [JsonProperty( "articulation_name")]
        public string ArticulationName { get; set; } = default!;

        [JsonProperty( "articulation_type" )]
        public ArticulationType ArticulationType { get; set; } = ArticulationType.Default;

        [JsonProperty( "articulation_group" )]
        public int ArticulationGroup { get; set; }
        [JsonProperty( "articulation_color")]
        public int ArticulationColor { get; set; }
        [JsonProperty("midi_message")]
        public MidiModel MidiMessage { get; set; } = new MidiModel();

        public ArticulationModel()
        {}

        public ArticulationModel(
            Guid id,
            DateTime created,
            DateTime lastUpdated,
            string developerName,
            string productName,
            string articulationName,
            ArticulationType articulationType,
            int articulationGroup,
            int articulationColor,
            MidiModel midiMessage )
        {
            Id                = id;
            Created           = created;
            LastUpdated       = lastUpdated;
            DeveloperName     = developerName;
            ProductName       = productName;
            ArticulationName  = articulationName;
            ArticulationType  = articulationType;
            ArticulationGroup = articulationGroup;
            ArticulationColor = articulationColor;
            MidiMessage       = midiMessage;
        }
    }
}