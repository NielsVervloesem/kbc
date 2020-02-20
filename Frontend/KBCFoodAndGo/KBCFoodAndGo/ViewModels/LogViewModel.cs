using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGo.Shared.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace KBCFoodAndGo.ViewModels
{
    public class LogViewModel : ViewModelBase
    {
        private List<Log> _logs;
        private readonly ILogDataService _logDataService;
        public LogViewModel(ILogDataService logDataService)
        {
            _logDataService = logDataService;

            ICommand setupCommand = new Command(async () => await GetLogs());
            setupCommand.Execute(null);

            PusherService.Pusher.Subscribe("logChannel");
            PusherService.Pusher.Bind("logEvent", UpdateLogs);
        }

        private void UpdateLogs(dynamic logs)
        {
            ICommand setupCommand = new Command(async () => await GetLogs());
            setupCommand.Execute(null);
        }

        public async Task GetLogs()
        {
            Logs = (await _logDataService.GetAllLogs()).ToList();
        }

        public List<Log> Logs
        {
            get => _logs;
            set
            {
                _logs = value;
                OnPropertyChanged();
            }
        }
    }
}
