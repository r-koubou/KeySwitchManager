using System;
using System.Collections.Generic;

using KeySwitchManager.Common.Utilities;

using LiteDB;

namespace Databases.LiteDB.KeySwitches.KeySwitches.Models
{
    public class KeySwitchModel
    {
        [BsonId]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Author { get; set; } = "Unknwon";
        public string Description { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTimeHelper.NowUtc();
        public DateTime LastUpdated { get; set; } = DateTimeHelper.NowUtc();
        public string DeveloperName { get; set; } = "Unknown";
        public string ProductName { get; set; } = "Unknown";
        public string InstrumentName { get; set; } = "Unknown";
        public IReadOnlyCollection<ArticulationModel> Articulations { get; set; } = new List<ArticulationModel>();
        public IReadOnlyDictionary<string, object> ExtraData { get; set; } = new Dictionary<string, object>();

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
            IReadOnlyDictionary<string, object> extraData )
        {
            Id             = id;
            Author         = author;
            Description    = description;
            Created        = created;
            LastUpdated    = lastUpdated;
            DeveloperName  = developerName;
            ProductName    = productName;
            InstrumentName = instrumentName;
            Articulations  = articulations;
            ExtraData      = extraData;
        }
    }
}