using System;
using SQLite;
using System.Collections.Generic;
using System.Text;

namespace NASA
{
    public interface ISQLite
    {
        string GetDatabasePath(string filename);
    }

    [Table("Astronauts")]
    public class Astronaut
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int Gender { get; set; }
        public string Nationality { get; set; }

        public string BirthYear { get; set; }

        public int Height { get; set; }
        public int Weight { get; set; }
        public int Eyes { get; set; }
        public int FlyTime { get; set; }
        public int Exams { get; set; }
        public string DoctorVerdict { get; set; }

        public string Schools { get; set; }
        public int WorkYears { get; set; }
        
        public string Password { get; set; }
    }
}
