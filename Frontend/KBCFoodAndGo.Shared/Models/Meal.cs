using Newtonsoft.Json;
using System;
using Xamarin.Forms;

namespace KBCFoodAndGo.Shared.Models
{
    public class Meal : Base, IEquatable<Meal>
    {
        private string _name;
        private string _base64Image;
        private string _imageUrl;
        private bool _isChecked;
        private Color _backgroundColor;

        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }
        [JsonProperty(PropertyName = "shortDescription")]
        public string ShortDescription { get; set; }

        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public Color BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                OnPropertyChanged();
            }
        }

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                OnPropertyChanged();
            }
        }

        public bool Equals(Meal other)
        {
            return other != null && this.Id == other.Id;
        }

        [JsonProperty(PropertyName = "imageBase64")]
        public string Base64Image
        {
            get => _base64Image;
            set
            {
                if (_base64Image == value) return;
                _base64Image = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl
        {
            get => _imageUrl;
            set
            {
                if (_imageUrl == value) return;
                _imageUrl = value;
                OnPropertyChanged();
            }
        }
    }
}
