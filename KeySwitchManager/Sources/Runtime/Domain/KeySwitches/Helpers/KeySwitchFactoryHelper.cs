using System;
using System.Collections.Generic;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Aggregations;
using KeySwitchManager.Domain.KeySwitches.Models.Factory;
using KeySwitchManager.Domain.MidiMessages.Models.Aggregations;
using KeySwitchManager.Domain.MidiMessages.Models.Factory;

namespace KeySwitchManager.Domain.KeySwitches.Helpers
{
    public static class KeySwitchFactoryHelper
    {
        public static KeySwitch CreateTemplate()
            => CreateTemplate(
                IKeySwitchFactory.Default,
                IArticulationFactory.Default,
                IMidiNoteOnFactory.Default,
                IMidiControlChangeFactory.Default,
                IMidiProgramChangeFactory.Default
            );

        public static KeySwitch CreateTemplate(
            IKeySwitchFactory factory,
            IArticulationFactory articulationFactory,
            IMidiNoteOnFactory midiNoteOnFactory,
            IMidiControlChangeFactory midiControlChangeFactory,
            IMidiProgramChangeFactory midiProgramChangeFactory )
            => factory.Create(
                Guid.NewGuid(),
                "Author",
                "Description",
                UtcDateTime.NowAsDateTime,
                UtcDateTime.NowAsDateTime,
                "Developer Name",
                "Product name",
                "Instrument name",
                new List<Articulation>
                {
                    articulationFactory.Create(
                        "name",
                        new List<IMidiChannelVoiceMessage>
                        {
                            midiNoteOnFactory.Create( 0, 100 )
                        },
                        new List<IMidiChannelVoiceMessage>
                        {
                            midiControlChangeFactory.Create( 1, 100 )
                        },
                        new List<IMidiChannelVoiceMessage>
                        {
                            midiProgramChangeFactory.Create( 23 )
                        },
                        new Dictionary<string, string>
                        {
                            { "extra1 key", "extra1 value" },
                            { "extra2 key", "extra2 value" },
                        }
                    ),
                },
                new Dictionary<string, string>
                {
                    { "extra1 key", "extra1 value" },
                    { "extra2 key", "extra2 value" },
                }
            );
    }
}
