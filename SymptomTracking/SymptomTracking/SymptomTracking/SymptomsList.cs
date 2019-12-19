using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SymptomTracking
{
    [Table("SymptomsList")]
    class SymptomsList
    {
        // PrimaryKey is typically numeric 
        /*[PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }*/

        [PrimaryKey, MaxLength(50)]
        public string SymptomItem { get; set; }
    }
}
