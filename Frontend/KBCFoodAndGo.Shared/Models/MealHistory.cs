using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KBCFoodAndGo.Shared.Models
{
    public class MealHistory
    {
        [JsonProperty(PropertyName = "mealList")]
        public List<Meal> MealList { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "date")]
        public string Date { get; set; }

        [JsonProperty(PropertyName = "totalPrice")]
        public Double TotalPrice { get; set; }
    }
}
