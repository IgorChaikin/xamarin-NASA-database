using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using Newtonsoft.Json;

[assembly: Dependency(typeof(NASA.Droid.FileWorker))]
namespace NASA.Droid
{
    public class FileWorker : IFileWorker
    {
        public Task<bool> ExistsAsync(string filename)
        {
            string filepath = GetFilePath(filename);
            bool exists = File.Exists(filepath);
            return Task<bool>.FromResult(exists);
        }

        public string LoadTextAsync(string filename)
        {
            string l;
            string filepath = GetFilePath(filename);
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader sr = new StreamReader(filepath))
            using (JsonReader reader = new JsonTextReader(sr))
                l = serializer.Deserialize<string>(reader);
            return l;
        }

        public Task SaveTextAsync(string filename, string text)
        {
            string filepath = GetFilePath(filename);
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(filepath))
            using (JsonWriter writer = new JsonTextWriter(sw))
                serializer.Serialize(writer, text);
            return Task.FromResult(true);
        }
 
        string GetFilePath(string filename)
        {
            return Path.Combine(GetDocsPath(), filename);
        }
        
        string GetDocsPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }
    }
}