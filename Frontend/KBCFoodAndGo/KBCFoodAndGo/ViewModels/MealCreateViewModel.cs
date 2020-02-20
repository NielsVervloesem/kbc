using FluentValidation;
using KBCFoodAndGo.Interfaces.Services;
using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.Shared.Models;
using Plugin.Media;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KBCFoodAndGo.ViewModels
{
    public class MealCreateViewModel : ViewModelBase
    {
        private Meal _meal;
        private Image _image;
        private string _title;
        private bool _enableAddButton;
        private readonly IMealDataService _mealDataService;
        private readonly AbstractValidator<Meal> _mealValidator;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;
        private readonly IMediaFileService _mediaFileService;
        public MealCreateViewModel(
            INavigationService navigationService,
            IMealDataService mealDataService,
            AbstractValidator<Meal> mealValidator,
            IDialogService dialogService,
            IMediaFileService mediaFileService)
        {
            _meal = new Meal();
            EnableAddButton = true;
            _navigationService = navigationService;
            _mealValidator = mealValidator;
            _mealDataService = mealDataService;
            _dialogService = dialogService;
            _mediaFileService = mediaFileService;
            SetViewTitle();
            SetDefaultImage();
        }

        public Command AddImage => new Command(async () => await SelectImage());
        public Command AddMeal => new Command(async () => await OnCreateMealAsync());
        public Command DeleteMeal => new Command(async () => await OnDeleteMeal());

        private async Task SelectImage()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await _dialogService.ShowDialog("Upload fout", "Foto selecteren niet mogelijk", "Ok");
            }
            var mediaFile = await _mediaFileService.GetImageMediaFileFromImagePicker();
            if (mediaFile != null)
            {
                _meal.Base64Image = await _mediaFileService.ConvertToBase64String(mediaFile);
                Image = _mediaFileService.GetImageFromMediaFile(mediaFile);
            }
        }

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

        public bool EnableAddButton
        {
            get => _enableAddButton;
            set
            {
                if (_enableAddButton == value) return;
                _enableAddButton = value;
                OnPropertyChanged();
            }
        }
        public string Title
        {
            get => _title;
            set
            {
                if (_title == value) return;
                _title = value;
                OnPropertyChanged();
            }
        }
        public Meal Meal
        {
            get => _meal;
            set
            {
                if (_meal == value) return;
                _meal = value;
                OnPropertyChanged();
            }
        }
        private async Task OnDeleteMeal()
        {
            var confirmmationResult = await _dialogService.ShowDialogTwoButtons("Bent u zeker dat u de wijzigingen ongedaan wilt maken?", "Maaltijd verwijderen", "Ja", "Nee");
            if (confirmmationResult && _meal != null)
            {
                _meal = null;
                await _navigationService.PopBackAsync();
            }
        }
        private async Task OnCreateMealAsync()
        {
            var results = _mealValidator.Validate(_meal);
            if (string.IsNullOrEmpty(_meal.Base64Image))
            {
                _meal.Base64Image = "defaultImage.jpg";
            }

            if (!results.IsValid)
            {
                await _dialogService.ShowDialog(results.ToString(), "Alert", "OK");
            }
            else
            {
                EnableAddButton = false;
                var confirmationResult = await _dialogService.ShowDialogTwoButtons("Bent u zeker dat u deze maaltijd wilt toevoegen?", "Maaltijd toevoegen", "Ja", "Nee");
                if (confirmationResult)
                {
                    await _mealDataService.AddMeal(_meal);
                    await _dialogService.ShowDialog("Maaltijd succesvol aangemaakt", "Success", "OK");
                    MessagingCenter.Instance.Send(this, "reloadList", true);
                    await _navigationService.PopBackAsync();
                }
                EnableAddButton = true;
            }
        }
        private void SetViewTitle()
        {
            _title = "Maaltijd Aanmaken";
        }
        private void SetDefaultImage()
        {
            ImageSource imageSource = ImageSource.FromFile("defaultImage.png");
            Image image = new Image
            {
                Aspect = Aspect.AspectFit,
                Source = imageSource
            };
            Image = image;
        }
    }
}
