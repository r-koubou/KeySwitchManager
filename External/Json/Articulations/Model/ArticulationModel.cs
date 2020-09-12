using System;
using System.Collections.Generic;

using ArticulationManager.Domain.Articulations.Value;

using Newtonsoft.Json;

namespace ArticulationManager.Json.Articulations.Model
{
    [JsonObject("articulation")]
    public class ArticulationModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty( "created")]
        public DateTime Created { get; set; }
        [JsonProperty( "last_updated")]
        public DateTime LastUpdated { get; set; }
        [JsonProperty( "developer_name")]
        public string DeveloperName { get; set; } = default!;
        [JsonProperty( "product_name")]
        public string ProductName { get; set; } = default!;
        [JsonProperty( "articulation_name")]
        public string ArticulationName { get; set; } = default!;
        [JsonProperty( "articulation_type")]
        public ArticulationType ArticulationType { get; set; }
        [JsonProperty( "articulation_group")]
        public int ArticulationGroup { get; set; }
        [JsonProperty( "articulation_color")]
        public int ArticulationColor { get; set; }
        public List<MidiMessageModel> NoteOn { get; set; } = default!;
        public List<MidiMessageModel> ControlChange { get; set; } = default!;
        public List<MidiMessageModel> ProgramChange { get; set; } = default!;

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
            List<MidiMessageModel> noteOn,
            List<MidiMessageModel> controlChange,
            List<MidiMessageModel> programChange )
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
            NoteOn            = new List<MidiMessageModel>( noteOn );
            ControlChange     = new List<MidiMessageModel>( controlChange );
            ProgramChange     = new List<MidiMessageModel>( programChange );
        }
    }
}