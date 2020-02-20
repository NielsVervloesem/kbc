using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGo.Shared.Services;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using Entry = Microcharts.Entry;

namespace KBCFoodAndGo.ViewModels
{
    public class StatisticsViewModel : ViewModelBase
    {
        private Chart _soldMealsTodayChart;
        private Chart _profitChart;
        private Chart _allTimeChart;

      

        private readonly IMealHistoryDataService _mealHistoryDataService;
        private Command setupCommand;

        public StatisticsViewModel(IMealHistoryDataService mealHistoryDataService)
        {
            _soldMealsTodayChart = new BarChart();
            _profitChart = new LineChart();
            _allTimeChart = new BarChart();
            
            _mealHistoryDataService = mealHistoryDataService;

            setupCommand = new Command(async () => await Setup());
            setupCommand.Execute(null);

            PusherService.Pusher.Subscribe("mealHistory");
            PusherService.Pusher.Bind("mealHistory", UpdateCharts);
        }

        private void UpdateCharts(dynamic obj)
        {
            setupCommand.Execute(null);
        }

        private async Task Setup()
        {
            await GetMealsToday();
            await GetProfitFromPastFiveDays();
            await GetAllTimeFavoriteMeals();
        }

        private async Task GetMealsToday()
        {
            List<ChartPoint> chartPoints = await _mealHistoryDataService.GetAllMealsFromToday();
            SoldMeals = new BarChart()
            {
                Entries = createChartPointsFromList(chartPoints, 0),
                LabelTextSize = 15.0f, 
                LabelColor = SKColor.Parse("#003665"),
                BackgroundColor = SKColors.Transparent, 
                ValueLabelOrientation = Orientation.Horizontal,
                LabelOrientation = Orientation.Horizontal,
                Margin = 20
            };
        }

        private async Task GetProfitFromPastFiveDays()
        {
            List<ChartPoint> chartPoints = await _mealHistoryDataService.GetProfitFromLastFiveDays();
            ProfitMeals = new LineChart()
            {
                Entries = createChartPointsFromList(chartPoints, 2),
                LabelTextSize = 18.0f,
                LabelColor = SKColor.Parse("#003665"),
                BackgroundColor = SKColors.Transparent,
                ValueLabelOrientation = Orientation.Horizontal,
                LabelOrientation = Orientation.Horizontal,
                Margin = 20
            };
        }

        private async Task GetAllTimeFavoriteMeals()
        {
            List<ChartPoint> chartPoints = await _mealHistoryDataService.GetAllTimeFavoriteMeals();
            AllTimeChart = new BarChart()
            {
                Entries = createChartPointsFromList(chartPoints, 0),
                LabelTextSize = 15.0f,
                LabelColor = SKColor.Parse("#003665"),
                BackgroundColor = SKColors.Transparent,
                Margin = 20,
                ValueLabelOrientation = Orientation.Horizontal,
                LabelOrientation = Orientation.Horizontal,
            };
        }


        public Chart SoldMeals
        {
            get => _soldMealsTodayChart;
            set
            {
                if (_soldMealsTodayChart == value) return;
                _soldMealsTodayChart = value;
                OnPropertyChanged();
            }
        }

        public Chart ProfitMeals
        {
            get => _profitChart;
            set
            {
                if (_profitChart == value) return;
                _profitChart = value;
                OnPropertyChanged();
            }
        }

        public Chart AllTimeChart
        {
            get => _allTimeChart;
            set
            {
                if (_allTimeChart == value) return;
                _allTimeChart = value;
                OnPropertyChanged();
            }
        }

        private Entry[] createChartPointsFromList(List<ChartPoint> chartPoints, int decimalsAmount)
        {
            var entries = new Entry[chartPoints.Count];

            int counter = 0;
            foreach (ChartPoint chartPoint in chartPoints)
            {
                entries[counter] = new Entry((float)chartPoint.Value)
                {
                    Color = SKColor.Parse("#00AEEF"),
                    Label = chartPoint.Label,
                    TextColor = SKColor.Parse("#003665"),
                    ValueLabel = Math.Round(chartPoint.Value, decimalsAmount) + ""
                };
                counter++;
            }

            return entries;
        }
    }
}
