using Newtonsoft.Json;

namespace Rtl.TvMazeScrapper.Infrastructure.Http.Model.Cast
{
    public class CastModel
    {
        [JsonProperty("person")]
        public CastPersonModel Person { get; set; }
    }
}
