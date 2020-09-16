using System;
using System.Collections.Generic;

using KeySwitchManager.Common.Utilities;

using LiteDB;

namespace KeySwitchManager.Databases.LiteDB.KeySwitches.Model
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
        public List<ArticulationModel> Articulations { get; set; } = new List<ArticulationModel>();

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
            IEnumerable<ArticulationModel> articulations )
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
        }
    }
}