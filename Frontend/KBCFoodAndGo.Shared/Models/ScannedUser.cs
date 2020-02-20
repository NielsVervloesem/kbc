using Newtonsoft.Json;

namespace KBCFoodAndGo.Shared.Models
{
    public class ScannedUser : Base
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
    }
}