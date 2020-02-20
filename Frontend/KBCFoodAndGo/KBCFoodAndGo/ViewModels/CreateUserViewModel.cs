

using FluentValidation;
using KBCFoodAndGo.Interfaces.Services;
using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.Shared.Models;
using Plugin.Media;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KBCFoodAndGo.ViewModels
{
    public class CreateUserViewModel: ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        private readonly IUserDataService _userDataService;
        private readonly AbstractValidator<User> _userValidator;
        private readonly IMediaFileService _mediaFileService;

        private User _user;

        public User User
        {
            get { return _user; }
            set { _user = value;
                OnPropertyChanged();
            }
        }

        private Image _image;
        public Image Image
        {
            get => _image;
            set
            {
                if (_image == value) return;
                _image = value;
                OnPropertyChanged();
            }
        }

        public CreateUserViewModel(INavigationService navigationService, IDialogService dialogService, IUserDataService userDataService, AbstractValidator<User> userValidator, IMediaFileService mediaFileService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _userDataService = userDataService;
            _mediaFileService = mediaFileService;
            _userValidator = userValidator;

            User = new User();

            ImageSource imageSource = ImageSource.FromFile("defaultImage.png");
            Image image = new Image
            {
                Aspect = Aspect.AspectFit,
                Source = imageSource
            };
            Image = image;
        }
        public ICommand AddUser => new Command(async () => await OnAddUser());

        private async Task OnAddUser()
        {
            var results = _userValidator.Validate(_user);

            if (!results.IsValid)
            {
                await _dialogService.ShowDialog(results.ToString(), "Alert", "OK");
            }
            else
            {
                var confirmationResult = await _dialogService.ShowDialogTwoButtons("Bent u zeker dat u deze gebruiker wilt toevoegen?", "Gebruiker toevoegen", "Ja", "Nee");
                if (confirmationResult)
                {
                    await _userDataService.CreateAsync(_user);
                    await _dialogService.ShowDialog("Gebruiker succesvol aangemaakt", "Success", "OK");
                    await _navigationService.NavigateToAsync<LoginViewModel>();
                }
            }
        }

        public Command AddImage => new Command(async () => await SelectImage());

        private async Task SelectImage()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await _dialogService.ShowDialog("Upload fout", "Foto selecteren niet mogelijk", "Ok");
            }
            var mediaFile = await _mediaFileService.GetImageMediaFileFromImagePicker();

            if (mediaFile != null)
            {
                _user.Base64Image = await _mediaFileService.ConvertToBase64String(mediaFile);
                Image = _mediaFileService.GetImageFromMediaFile(mediaFile);
            }
        }

        public Command GoBack => new Command(async () => await NavigateToLogin());

        private async Task NavigateToLogin()
        {
            var confirmationResult = await _dialogService.ShowDialogTwoButtons("Bent u zeker dat u terug wilt gaan?", "Terug gaan", "Ja", "Nee");
            if (confirmationResult)
            {
                await _navigationService.NavigateToAsync<LoginViewModel>();
            }
        }

    }
}

