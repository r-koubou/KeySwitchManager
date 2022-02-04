using System;
using System.Collections.Generic;

using RkHelper.Time;

namespace KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Models.Aggregations
{
    public class KeySwitchModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Author { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string DeveloperName { get; set; } = default!;

        public string ProductName { get; set; } = default!;

        public string InstrumentName { get; set; } = default!;

        public IList<ArticulationModel> Articulations { get; set; } = new List<ArticulationModel>();

        public IDictionary<string, string> ExtraData { get; set; } = new Dictionary<string, string>();

        public KeySwitchModel()
        {}

        public KeySwitchModel(
            Guid id,
            string author,
            string description,
            string developerName,
            string productName,
            string instrumentName,
            IReadOnlyCollection<ArticulationModel> articulations,
            IReadOnlyDictionary<string, string> extraData )
        {
            Id             = id;
            Author         = author;
            Description    = description;
            DeveloperName  = developerName;
            ProductName    = productName;
            InstrumentName = instrumentName;
            Articulations  = new List<ArticulationModel>( articulations );
            ExtraData      = new Dictionary<string, string>( extraData );
        }
    }
}