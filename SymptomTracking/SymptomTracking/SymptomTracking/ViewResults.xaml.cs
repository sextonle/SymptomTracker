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
    public partial class ViewResults : ContentPage
    {
        public ViewResults()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var allRecords = from s in DB.conn.Table<SymptomsTracker>()
                             orderby s.BodyPart
                             select s;
            List<string> l = new List<string>();
            foreach (var i in allRecords)
            {
                l.Add(i.Pain.ToString() + " - " + i.BodyPart + " - " + i.CurrentDate.Date.ToShortDateString());
            }
            allRecordsDisplay.ItemsSource = l;
        }
    }
}