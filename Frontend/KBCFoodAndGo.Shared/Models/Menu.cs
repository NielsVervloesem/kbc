using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace KBCFoodAndGo.Shared.Models
{
    public class Menu : Base
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }
        [JsonProperty(PropertyName = "meals")]
        public ObservableCollection<Meal> Meals { get; set; }
    }
}
