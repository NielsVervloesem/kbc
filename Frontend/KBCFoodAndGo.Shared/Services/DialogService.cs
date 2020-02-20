using Acr.UserDialogs;
using KBCFoodAndGo.Shared.Interfaces.Services;
using System.Threading.Tasks;

namespace KBCFoodAndGo.Shared.Services
{
    public class DialogService : IDialogService
    {
        public Task ShowDialog(string message, string title, string buttonLabel)
        {
            return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }

        public Task<bool> ShowDialogTwoButtons(string message, string title, string confirmButtonText, string cancelButtonText)
        {
            return UserDialogs.Instance.ConfirmAsync(message, title, confirmButtonText, cancelButtonText);
        }

        public void ShowToast(string message)
        {
            UserDialogs.Instance.Toast(message);
        }
    }
}
