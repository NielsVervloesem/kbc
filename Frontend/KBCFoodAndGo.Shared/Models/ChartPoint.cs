using System;
using Newtonsoft.Json;

namespace KBCFoodAndGo.Shared.Models
{
    public class ChartPoint : Base, IEquatable<ChartPoint>
    {
        private string _label;
        [JsonProperty(PropertyName = "label")]
        public string Label
        {
            get => _label;
            set
            {
                if (_label == value) return;
                _label = value;
                OnPropertyChanged();
            }
        }

        private double _value;
        [JsonProperty(PropertyName = "value")]
        public double Value
        {
            get => _value;
            set
            {
                if (_value == value) return;
                _value = value;
                OnPropertyChanged();
            }
        }


        public bool Equals(ChartPoint other)
        {
            return other != null && other.Value == this.Value && other.Label == this.Label;
        }
    }
}
