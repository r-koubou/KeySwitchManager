using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Domain.MidiMessages.Aggregate;

namespace KeySwitchManager.UseCases.KeySwitches.Adding
{
    public class KeySwitchAddingRequest
    {
        public string Author { get; }
        public string Description { get; }
        public string DeveloperName { get; }
        public string ProductName { get; }
        public string InstrumentName { get; }
        public string ArticulationName { get; }
        public ArticulationType ArticulationType { get; }
        public int ArticulationGroup { get; }
        public int ArticulationColor { get; }
        public IEnumerable<MidiNoteOn> MidiNoteOns { get; }
        public IEnumerable<MidiControlChange> MidiControlChanges { get; }
        public IEnumerable<MidiProgramChange> MidiProgramChanges { get; }

        public KeySwitchAddingRequest(
            string author,
            string description,
            string developerName,
            string productName,
            string instrumentName,
            string articulationName,
            ArticulationType articulationType,
            int articulationGroup,
            int articulationColor,
            IEnumerable<MidiNoteOn> midiNoteOns,
            IEnumerable<MidiControlChange> midiControlChanges,
            IEnumerable<MidiProgramChange> midiProgramChanges )
        {
            Author             = author;
            Description        = description;
            DeveloperName      = developerName;
            ProductName        = productName;
            InstrumentName     = instrumentName;
            ArticulationName   = articulationName;
            ArticulationType   = articulationType;
            ArticulationGroup  = articulationGroup;
            ArticulationColor  = articulationColor;
            MidiNoteOns        = midiNoteOns;
            MidiControlChanges = midiControlChanges;
            MidiProgramChanges = midiProgramChanges;
        }
    }
}