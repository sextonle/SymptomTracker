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
    public partial class DBSymptomInsertDelete : ContentPage
    {
        int updatedId;
        public DBSymptomInsertDelete()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            refreshRecords();
            var allSymptoms = from s in DB.conn.Table<SymptomsList>()
                              select s.SymptomItem;
            UpdatePickAvilSymptoms.ItemsSource = allSymptoms.ToList();
            UpdateFeedback.Text = "";
        }

        public void refreshRecords ()
        {
            var allRecords = from s in DB.conn.Table<SymptomsTracker>()
                             select s;
            List<string> l = new List<string>();
            foreach (var i in allRecords)
            {
                l.Add(i.CurrentDate.ToString() + "," + i.BodyPart + "," + i.Pain.ToString());
            }
            dataB.ItemsSource = l;
        }

        private void DeleteRecordClicked(object sender, EventArgs e)
        {
            if (dataB.SelectedItem == null)
            {
                UpdateFeedback.Text = "Please select a record";
                return;
            }

            string selected = dataB.SelectedItem.ToString();
            string[] arrSelected = selected.Split(',');

            var date = Convert.ToDateTime(arrSelected[0]);
            var bp = arrSelected[1];
            var hurt = Convert.ToInt32(arrSelected[2]);

            var deleteRecord = from rec in DB.conn.Table<SymptomsTracker>()
                               where rec.CurrentDate == date
                               where rec.BodyPart == bp
                               where rec.Pain == hurt
                               select rec.Id;
            var id = deleteRecord.FirstOrDefault();

            SymptomsTracker oldRecord = new SymptomsTracker
            {
                Id = id,
                CurrentDate = Convert.ToDateTime(arrSelected[0]),
                BodyPart = arrSelected[1],
                Pain = Convert.ToInt32(arrSelected[2])
            };

            DB.conn.Delete(oldRecord);
            refreshRecords();
        }

        private void UpdateRecordClicked(object sender, EventArgs e)
        {
            if (dataB.SelectedItem == null)
            {
                UpdateFeedback.Text = "Please select a record";
                return;
            }

            hiddenStackLayout.IsVisible = true;

            //getting what was selected
            string selectedEntry = dataB.SelectedItem.ToString();
            string[] arrSelectedEntry = selectedEntry.Split(',');

            var date1 = Convert.ToDateTime(arrSelectedEntry[0]);
            var bp1 = arrSelectedEntry[1];
            var hurt1 = Convert.ToInt32(arrSelectedEntry[2]);

            //getting the curent ID
            var deleteRecord1 = from rec in DB.conn.Table<SymptomsTracker>()
                                where rec.CurrentDate == date1
                                where rec.BodyPart == bp1
                                where rec.Pain == hurt1
                                select rec.Id;
            updatedId = deleteRecord1.FirstOrDefault();

            //populating items with current data
            UpdateSymptomDate.Date = date1;
            UpdatePickAvilSymptoms.SelectedItem = bp1;
            UpdateSymptomPain.SelectedItem = arrSelectedEntry[2];
        }

        private void FinalUpdateClicked(object sender, EventArgs e)
        {
            
            SymptomsTracker UpdateRecord = new SymptomsTracker
            {
                Id = updatedId,
                CurrentDate = UpdateSymptomDate.Date,
                BodyPart = UpdatePickAvilSymptoms.SelectedItem.ToString(),
                Pain = Convert.ToInt32(UpdateSymptomPain.SelectedItem)
            };
            DB.conn.Update(UpdateRecord);
            hiddenStackLayout.IsVisible = false;
            refreshRecords();
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            hiddenStackLayout.IsVisible = false;
        }
    }
}