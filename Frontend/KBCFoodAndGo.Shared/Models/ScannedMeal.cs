using Newtonsoft.Json;


namespace KBCFoodAndGo.Shared.Models
{
    public class ScannedMeal : Base
    {
        [JsonProperty(PropertyName = "id")]

        public int Id { get; set; }
    }
}
