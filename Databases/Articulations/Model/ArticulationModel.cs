using System;
using System.Collections.Generic;

namespace ArticulationManager.Databases.Articulations.Model
{
    public class ArticulationModel
    {
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public string DeveloperName { get; set; } = "Unknown";
        public string ProductName { get; set; } = "Unknown";
        public string ArticulationName { get; set; } = "Unknown";
        public string ArticulationType { get; set; } = Domain.Articulations.Value.ArticulationType.Default.ToString();
        public int ArticulationGroup { get; set; }
        public int ArticulationColor { get; set; }
        public List<MidiMessageModel> NoteOn { get; set; } = new List<MidiMessageModel>();
        public List<MidiMessageModel> ControlChange { get; set; } = new List<MidiMessageModel>();
        public List<MidiMessageModel> ProgramChange { get; set; } = new List<MidiMessageModel>();

        public ArticulationModel()
        {}

        public ArticulationModel(
            DateTime created,
            DateTime lastUpdated,
            string developerName,
            string productName,
            string articulationName,
            int articulationGroup,
            int articulationColor )
        {
            DeveloperName     = developerName;
            ProductName       = productName;
            ArticulationName  = articulationName;
            ArticulationGroup = articulationGroup;
            ArticulationColor = articulationColor;
            Created           = created;
            LastUpdated       = lastUpdated;
        }
    }
}