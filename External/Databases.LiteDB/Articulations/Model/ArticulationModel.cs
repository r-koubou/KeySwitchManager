using System;
using System.Collections.Generic;

using ArticulationManager.Common.Utilities;

using LiteDB;

namespace ArticulationManager.Databases.LiteDB.Articulations.Model
{
    public class ArticulationModel
    {
        [BsonId]
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime Created { get; set; } = DateTimeHelper.NowUtc();
        public DateTime LastUpdated { get; set; } = DateTimeHelper.NowUtc();
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
            int articulationColor,
            IEnumerable<MidiMessageModel> midiNoteOns,
            IEnumerable<MidiMessageModel> midiControlChanges,
            IEnumerable<MidiMessageModel> midiProgramChanges )
        {
            DeveloperName     = developerName;
            ProductName       = productName;
            ArticulationName  = articulationName;
            ArticulationGroup = articulationGroup;
            ArticulationColor = articulationColor;
            Created           = created;
            LastUpdated       = lastUpdated;
            NoteOn            = new List<MidiMessageModel>( midiNoteOns );
            ControlChange     = new List<MidiMessageModel>( midiControlChanges );
            ProgramChange     = new List<MidiMessageModel>( midiProgramChanges );
        }
    }
}