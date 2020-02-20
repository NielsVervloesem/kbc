using FluentValidation;
using KBCFoodAndGo.Interfaces.Services;
using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGo.Views;
using Plugin.Media;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KBCFoodAndGo.ViewModels
{
    public class MealEditViewModel : ViewModelBase
    {
        private Image _image;
        private Meal _meal;
        private string _title;
        private readonly IMealDataService _mealDataService;
        private readonly AbstractValidator<Meal> _mealValidator;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;
        private readonly IMediaFileService _mediaFileService;

        public MealEditViewModel(
            INavigationService navigationService,
            IMealDataService mealDataService,
            AbstractValidator<Meal> mealValidator,
            IDialogService dialogService,
            IMediaFileService mediaFileService)
        {
            _navigationService = navigationService;
            _mealValidator = mealValidator;
            _mealDataService = mealDataService;
            _dialogService = dialogService;
            _mediaFileService = mediaFileService;
            MessagingCenter.Instance.Subscribe<MealDetailViewModel, Meal>(this, "sendMeal",
                (sender, meal) =>
                {
                    Meal = meal;
                    CheckMealImage(Meal);
                });
        }

        private void CheckMealImage(Meal meal)
        {
            if (string.IsNullOrEmpty(meal.ImageUrl))
            {
                SetDefaultImage();
            }
            else
            {
                SetMealImage();
            }
        }

        private void SetViewTitle()
        {
            if (_meal != null)
            {
                _title = $"Aanpassen {Meal.Name}";
            }
        }
        public Command AddImage => new Command(async () => await SelectImage());

        public Command AddMeal => new Command(async () => await OnUpdateMealAsync());
        public Command DeleteMeal => new Command(async () => await OnDeleteMealAsync());

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
                SetViewTitle();
                OnPropertyChanged();
            }
        }


        private async Task OnDeleteMealAsync()
        {
            var confirmmationResult = await _dialogService.ShowDialogTwoButtons("Bent u zeker dat u deze maaltijd wilt verwijderen?", "Maaltijd verwijderen", "Ja", "Nee");
            if (confirmmationResult && _meal != null)
            {
                await _mealDataService.DeleteMeal(_meal.Id);
                await _navigationService.PopUntilDestination(typeof(HomeView));
                MessagingCenter.Instance.Send(this, "reloadList", true);
            }
        }
        private async Task OnUpdateMealAsync()
        {

            var results = _mealValidator.Validate(_meal);

            if (!results.IsValid)
            {
                await _dialogService.ShowDialog(results.ToString(), "Alert", "OK");
            }
            else
            {
                var confirmmationResult = await _dialogService.ShowDialogTwoButtons("Bent u zeker dat u deze aanpassingen wilt aanbrengen?", "Maaltijd aanpassen", "Ja", "Nee");
                if (confirmmationResult)
                {
                    await _mealDataService.UpdateMeal(_meal.Id, _meal);
                    MessagingCenter.Instance.Send(this, "reloadList", true);
                    await _dialogService.ShowDialog("Maaltijd succesvol aangepast", "Success", "OK");
                    await _navigationService.PopUntilDestination(typeof(HomeView));
                }
            }
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

        private void SetMealImage()
        {
            Image image = new Image
            {
                Aspect = Aspect.AspectFit,
                Source = _meal.ImageUrl
            };
            Image = image;
        }
    }
}
