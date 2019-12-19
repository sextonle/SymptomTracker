using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;

namespace SymptomTracking
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MySymptoms : ContentPage
    {
        public ObservableCollection<string> symptoms;
        public MySymptoms()
        {
            InitializeComponent();
            DB.OpenConnection();
            symptoms = new ObservableCollection<string>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var allSymptoms = from s in DB.conn.Table<SymptomsList>()
                              select s.SymptomItem;
            addedSymptoms.ItemsSource = allSymptoms.ToList();
            tooMany.Text = "";
        }

        private void AddSymptomClicked(object sender, EventArgs e)
        {
            if(symptomPicker.SelectedItem == null)
            {
                tooMany.Text = "Please select a symptom to track";
                return;
            }
            string itemAdd = symptomPicker.SelectedItem.ToString();
            var currentSymptoms = from s in DB.conn.Table<SymptomsList>()
                             select s.SymptomItem;
            if(currentSymptoms.Count() >= 5)
            {
                tooMany.Text = "You can only track up to 5 symptoms.";
            }
            
            else if(currentSymptoms.Count() < 5 && (!currentSymptoms.Contains(itemAdd)))
            {
                tooMany.Text = "";

                SymptomsList newSymptom = new SymptomsList
                {
                    SymptomItem = itemAdd
                };
                DB.conn.Insert(newSymptom);

                var loadedSymp = from s in DB.conn.Table<SymptomsList>()
                           select s.SymptomItem;
                addedSymptoms.ItemsSource = loadedSymp.ToList();
            }
            
        }
        private void RemoveSymptomClicked(object sender, EventArgs e)
        {
            if (symptomPicker.SelectedItem == null)
            {
                tooMany.Text = "Please select a symptom to remove";
                return;
            }
            tooMany.Text = "";
            var currentSymptoms = from s in DB.conn.Table<SymptomsList>()
                                  select s.SymptomItem;
            var itemDelete1 = symptomPicker.SelectedItem;
            string itemDelete = itemDelete1.ToString();

            if (currentSymptoms.Count() > 0 && currentSymptoms.Contains(itemDelete))
            {
                SymptomsList oldSymptom = new SymptomsList
                {
                    SymptomItem = itemDelete
                };
                DB.conn.Delete(oldSymptom);
                var loadedSymp = from s in DB.conn.Table<SymptomsList>()
                                 select s.SymptomItem;
                addedSymptoms.ItemsSource = loadedSymp.ToList();
            }
            else
            {
                tooMany.Text = "Item not being tracked";
            }
        }
    }
}