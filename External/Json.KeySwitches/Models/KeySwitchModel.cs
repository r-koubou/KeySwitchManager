using System;
using System.Collections.Generic;

using KeySwitchManager.Common.Utilities;

using Newtonsoft.Json;

namespace KeySwitchManager.Json.KeySwitches.Models
{
    [JsonObject("instrument")]
    public class KeySwitchModel
    {
        [JsonProperty( "id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty( "author")]
        public string Author { get; set; } = default!;

        [JsonProperty( "description")]
        public string Description { get; set; } = default!;

        [JsonProperty( "created" )]
        public DateTime Created { get; set; } = DateTimeHelper.NowUtc();

        [JsonProperty( "last_updated")]
        public DateTime LastUpdated { get; set; } = DateTimeHelper.NowUtc();

        [JsonProperty( "developer_name")]
        [JsonRequired]
        public string DeveloperName { get; set; } = default!;

        [JsonProperty( "product_name")]
        [JsonRequired]
        public string ProductName { get; set; } = default!;

        [JsonProperty( "instrument_name")]
        [JsonRequired]
        public string InstrumentName { get; set; } = default!;

        [JsonProperty( "articulations")]
        public IReadOnlyCollection<ArticulationModel> Articulations { get; set; } = new List<ArticulationModel>();

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
            IReadOnlyCollection<ArticulationModel> articulations )
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
        }
    }
}