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
        public IReadOnlyCollection<MidiNoteOn> MidiNoteOns { get; }
        public IReadOnlyCollection<MidiControlChange> MidiControlChanges { get; }
        public IReadOnlyCollection<MidiProgramChange> MidiProgramChanges { get; }
        public IReadOnlyDictionary<string, string> ExtraData { get; }

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
            IReadOnlyCollection<MidiNoteOn> midiNoteOns,
            IReadOnlyCollection<MidiControlChange> midiControlChanges,
            IReadOnlyCollection<MidiProgramChange> midiProgramChanges,
            IReadOnlyDictionary<string, string> extraData )
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
            ExtraData          = extraData;
        }
    }
}