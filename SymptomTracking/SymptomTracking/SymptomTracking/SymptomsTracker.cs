using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SymptomTracking
{
    [Table("SymptomsTracker")]
    public class SymptomsTracker
    {
        // PrimaryKey is typically numeric 
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        [MaxLength(100)]
        public DateTime CurrentDate { get; set; }

        [MaxLength(50)]
        public string BodyPart { get; set; }

        [MaxLength(5)]
        public int Pain { get; set; }

    }
}
