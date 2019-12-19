using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SymptomTracking
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewPage : ContentPage
    {
        public ViewPage()
        {
            InitializeComponent();
            
        }
        ViewResults vr;
        private async void newPageClicked(object sender, EventArgs e)
        {
            vr = new ViewResults();
            await Navigation.PushAsync(vr, false);
            
        }
    }
}