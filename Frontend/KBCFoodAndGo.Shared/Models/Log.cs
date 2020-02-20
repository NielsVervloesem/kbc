using Newtonsoft.Json;

namespace KBCFoodAndGo.Shared.Models
{
    public class Log : Base
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "date")]
        public string Date { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

    }
}
