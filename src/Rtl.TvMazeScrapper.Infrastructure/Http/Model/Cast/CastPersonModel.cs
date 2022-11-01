using Newtonsoft.Json;

namespace Rtl.TvMazeScrapper.Infrastructure.Http.Model.Cast
{
    public class CastPersonModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("birthday")]
        public string Birthday { get; set; }
    }
}
