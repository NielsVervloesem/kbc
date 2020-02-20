using System.Threading.Tasks;

namespace KBCFoodAndGo.Shared.Interfaces.Services
{
    public interface IDialogService
    {
        Task ShowDialog(string message, string title, string buttonLabel);

        Task<bool> ShowDialogTwoButtons(string message, string title, string confirmButtonText, string cancelButtonText);

        void ShowToast(string message);
    }
}
