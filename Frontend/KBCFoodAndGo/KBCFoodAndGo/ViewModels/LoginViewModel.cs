using KBCFoodAndGo.Interfaces.Services;
using KBCFoodAndGo.Shared.Exceptions;
using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGo.Shared.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KBCFoodAndGo.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IUserDataService _userDataService;

        private User _user;

        public LoginViewModel(INavigationService navigationService, IUserDataService userDataService)
        {
            LocalStorage.InitializeStorage();

            _navigationService = navigationService;
            _userDataService = userDataService;

            _user = new User();
            User.Email = "admin@hotmail.com";
            User.Password = "test";

            Enabled = true;
        }
        public ICommand NavigateToCreateUser => new Command(async () => await CreateUser());
        private async Task CreateUser()
        {
            await _navigationService.NavigateToAsync<CreateUserViewModel>();

        }

        public ICommand NavigateToCommand => new Command(async () => await Login());
        private async Task Login()
        {
            Enabled = false;
            try
            {
                var user = await _userDataService.LoginAsync(_user);

                LocalStorage.Add("UserRole", user.Role);
                LocalStorage.Add("Id", user.Id);

                if (user.Role == "ADMIN")
                {
                    await _navigationService.NavigateToAsync<HomeViewModel>();
                }
                else if (user.Role == "CUSTOMER" || user.Role == "CAFETARIA_EMPLOYEE")
                {
                    await _navigationService.NavigateToAsync<MenuViewModel>();
                }       
            }
            catch (NotFoundException)
            {
                IsVisible = true;
                ErrorMessage = "E-mailadres niet gevonden!";
                Enabled = true;
            }
            catch (BadRequestException)
            {
                IsVisible = true;
                ErrorMessage = "Foutief wachtwoord!";
                Enabled = true;
            }
        }

        public User User
        {
            get => _user;
            set
            {
                if (_user == value) return;
                _user = value;
                OnPropertyChanged();
            }
        }

        private bool _isVisible = false;
        public bool IsVisible
        {
            get => _isVisible;
            set { _isVisible = value; OnPropertyChanged(); }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        private bool _enabled;
        public bool Enabled
        {
            get => _enabled;
            set { _enabled = value; OnPropertyChanged(); }
        }
    }
}