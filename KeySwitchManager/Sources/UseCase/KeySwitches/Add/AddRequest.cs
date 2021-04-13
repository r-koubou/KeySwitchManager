using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Midi.Models.Entities;

namespace KeySwitchManager.UseCase.KeySwitches.Add
{
    public class AddRequest
    {
        public string Author { get; }
        public string Description { get; }
        public string DeveloperName { get; }
        public string ProductName { get; }
        public string InstrumentName { get; }
        public string ArticulationName { get; }
        public IReadOnlyCollection<MidiNoteOn> MidiNoteOns { get; }
        public IReadOnlyCollection<MidiControlChange> MidiControlChanges { get; }
        public IReadOnlyCollection<MidiProgramChange> MidiProgramChanges { get; }
        public IReadOnlyDictionary<string, string> ExtraData { get; }

        public AddRequest(
            string author,
            string description,
            string developerName,
            string productName,
            string instrumentName,
            string articulationName,
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
            MidiNoteOns        = midiNoteOns;
            MidiControlChanges = midiControlChanges;
            MidiProgramChanges = midiProgramChanges;
            ExtraData          = extraData;
        }
    }
}