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
    public partial class DBSymptomCRUD : ContentPage
    {
        public DBSymptomCRUD()
        {
            InitializeComponent();    
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var allSymptoms = from s in DB.conn.Table<SymptomsList>()
                                select s.SymptomItem;
            pickAvilSymptoms.ItemsSource = allSymptoms.ToList();
            addFeedback.Text = "";
        }

        private void AddEventClicked(object sender, EventArgs e)
        {
            if (symptomDate.Date == null || pickAvilSymptoms.SelectedItem == null || symptomPain.SelectedItem == null)
            {
                addFeedback.Text = "Please fill out all above fields";
                return;
            }
            addFeedback.Text = "";
           SymptomsTracker newRecord = new SymptomsTracker
           {
                CurrentDate = symptomDate.Date,
                BodyPart = pickAvilSymptoms.SelectedItem.ToString(),
                Pain = Convert.ToInt32(symptomPain.SelectedItem)
            };
            try
            {
                DB.conn.Insert(newRecord);
            }
            catch (Exception)
            {
                addFeedback.Text = "Unable to add record";
            }
            
            
            addFeedback.Text = "Pain level " + Convert.ToInt32(symptomPain.SelectedItem) + 
                " in your " + pickAvilSymptoms.SelectedItem.ToString() + " on " + 
                symptomDate.Date.ToShortDateString() + " has been recorded.";
        }
    }
}