using System;
using System.Collections.Generic;

using ArticulationManager.Common.Utilities;

using Newtonsoft.Json;

namespace ArticulationManager.Json.Articulations.Model
{
    [JsonObject("instrument")]
    public class KeySwitchModel
    {
        [JsonProperty( "id")]
        public Guid Id { get; set; } = Guid.NewGuid();

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

        [JsonProperty( "articulation")]
        public List<ArticulationModel> Articulations { get; set; } = new List<ArticulationModel>();

        public KeySwitchModel()
        {}

        public KeySwitchModel(
            DateTime created,
            DateTime lastUpdated,
            string developerName,
            string productName,
            string instrumentName,
            IEnumerable<ArticulationModel> articulations )
        {
            Created        = created;
            LastUpdated    = lastUpdated;
            DeveloperName  = developerName;
            ProductName    = productName;
            InstrumentName = instrumentName;
            Articulations  = new List<ArticulationModel>( articulations );
        }
    }
}