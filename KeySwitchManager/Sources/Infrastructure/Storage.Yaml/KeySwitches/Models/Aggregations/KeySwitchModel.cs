using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using RkHelper.Time;

namespace KeySwitchManager.Storage.Yaml.KeySwitches.Models.Aggregations
{
    public class KeySwitchModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Author { get; set; } = default!;

        public string Description { get; set; } = default!;

        public DateTime Created { get; set; } = DateTimeHelper.NowUtc();

        public DateTime LastUpdated { get; set; } = DateTimeHelper.NowUtc();

        [Required]
        public string DeveloperName { get; set; } = default!;

        [Required]
        public string ProductName { get; set; } = default!;

        [Required]
        public string InstrumentName { get; set; } = default!;

        public IList<ArticulationModel> Articulations { get; set; } = new List<ArticulationModel>();

        public IDictionary<string, string> ExtraData { get; set; } = new Dictionary<string, string>();

        public KeySwitchModel()
        {}

        public KeySwitchModel(
            Guid id,
            string author,
            string description,
            DateTime created,
            DateTime lastUpdated,
            string developerName,
            string productName,
            string instrumentName,
            IReadOnlyCollection<ArticulationModel> articulations,
            IReadOnlyDictionary<string, string> extraData )
        {
            Id             = id;
            Author         = author;
            Description    = description;
            Created        = created;
            LastUpdated    = lastUpdated;
            DeveloperName  = developerName;
            ProductName    = productName;
            InstrumentName = instrumentName;
            Articulations  = new List<ArticulationModel>( articulations );
            ExtraData      = new Dictionary<string, string>( extraData );
        }
    }
}