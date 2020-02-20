using KBCFoodAndGoResto.Helpers;
using KBCFoodAndGoResto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KBCFoodAndGoResto.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NumPadView : ContentPage
    {
        public NumPadView()
        {
            InitializeComponent();
            BindingContext = AppContainer.Resolve<NumPadViewModel>();
        }
    }
}