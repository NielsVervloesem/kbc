using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace KBCFoodAndGo.Shared.Models
{
    public class User : Base, IEquatable<User>
    {
        private long _id;
        private string _lastName;
        private string _firstName;
        private string _password;
        private string _role;
        private string _imageUrl;
        private string _base64Image;


        public User(long id, string email, string password, string role)
        {
            _id = id;
            _lastName = email;
            _password = password;
            _role = role;
        }
        public User() { }


        private string _email;
        [JsonProperty(propertyName: "email")]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        [JsonProperty(propertyName: "firstName")]
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName
                    == value) return;
                _firstName = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty(propertyName: "lastName")]
        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName
                    == value) return;
                _lastName = value;
                OnPropertyChanged();
            }
        }

        private List<MealHistory> _mealHistory;
        [JsonProperty(propertyName: "mealHistory")]
        public List<MealHistory> MealHistory
        {
            get => _mealHistory;
            set
            {
                if (_mealHistory
                    == value) return;
                _mealHistory = value;
                OnPropertyChanged();
            }
        }
        [JsonProperty(PropertyName = "password")]
        public string Password
        {
            get => _password;
            set
            {
                if (_password == value) return;
                _password = value;
                OnPropertyChanged();
            }
        }
        public string Role
        {
            get => _role;
            set
            {
                if (_role == value) return;
                _role = value;
                OnPropertyChanged();
            }
        }

        public long Id
        {
            get => _id;
            set
            {
                if (_id == value) return;
                _id = value;
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

        private Color _backgroundColor;
        public Color BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                OnPropertyChanged();
            }
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

        public bool Equals(User other)
        {
            return other != null && this.Id == other.Id;
        }
    }
}