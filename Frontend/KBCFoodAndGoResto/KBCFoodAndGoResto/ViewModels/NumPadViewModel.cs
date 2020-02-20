using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGoResto.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KBCFoodAndGoResto.ViewModels
{
    public class NumPadViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public string Password { get; set; }
        private string _inputCode;
        public string InputCode
        {
            get { return _inputCode; }
            set
            {
                _inputCode = value;
                OnPropertyChanged();
            }
        }

        public Command EnterCodeCommand => new Command<string>(OnSelectionChange);
        public Command ClearCommand => new Command(ClearInputCode);

        private void ClearInputCode()
        {
            InputCode = "";
        }
        
        public Command OkCommand => new Command(async () => await CheckInputCode());

        public List<ScannedUser> Users { get; set; }
        public List<ScannedMeal> Meals { get; set; }

        private async Task CheckInputCode()
        {
            if(InputCode == Password)
            {
                await _navigationService.NavigateToAsync<EmployeeHelpViewModel>();
                MessagingCenter.Send(this, "sendScannedUsers", Users);
                MessagingCenter.Send(this, "sendScannedMeals", Meals);
            }
        }

        private void OnSelectionChange(string number)
        {
            InputCode += number;
        }


        public NumPadViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            MessagingCenter.Subscribe(this, "Code", (ScanResultViewModel vm, string code) =>
            {
                Password = code;
            });

            MessagingCenter.Subscribe(this, "UsersToNumPad", (ScanResultViewModel vm, List<ScannedUser> users) =>
            {
                Users = users;
            });

            MessagingCenter.Subscribe(this, "MealsToNumPad", (ScanResultViewModel vm, List<ScannedMeal> meals) =>
            {
                Meals = meals;
            });
        }
    }
}
