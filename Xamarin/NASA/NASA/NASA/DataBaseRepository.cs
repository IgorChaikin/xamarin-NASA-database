using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using Xamarin.Forms;
using System.IO;

namespace NASA
{
    public class DataBaseRepository
    {
        SQLiteConnection database;

        public DataBaseRepository(string filename)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, filename);
            database = new SQLiteConnection(path);
            database.CreateTable<Astronaut>();
        }

        public IEnumerable<Astronaut> GetItems()
        {
            return (from i in database.Table<Astronaut>() select i).ToList();
        }

        public Astronaut GetItem(int id)
        {
            return database.Get<Astronaut>(id);
        }

        public int DeleteItem(int id)
        {
            return database.Delete<Astronaut>(id);
        }

        public int SaveItem(Astronaut item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }
}

