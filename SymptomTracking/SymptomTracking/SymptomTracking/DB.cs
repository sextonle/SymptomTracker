using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace SymptomTracking
{
    
    class DB
    {
        public static SQLiteConnection conn;
        public static void OpenConnection()
        {
            string libFolder = FileSystem.AppDataDirectory;
            string fname = System.IO.Path.Combine(libFolder, "SymptomLog.db");
            conn = new SQLiteConnection(fname);
            /*conn.DropTable<SymptomsTracker>();
            conn.DropTable<SymptomsList>();*/
            conn.CreateTable<SymptomsTracker>();
            conn.CreateTable<SymptomsList>();
        }
        
    }
}
