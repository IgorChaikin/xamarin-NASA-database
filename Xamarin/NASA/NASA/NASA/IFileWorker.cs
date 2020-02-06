using System.Collections.Generic;
using System.Threading.Tasks;

namespace NASA
{
    public interface IFileWorker
    {
        Task<bool> ExistsAsync(string filename);
        Task SaveTextAsync(string filename, string text);
        string LoadTextAsync(string filename);
    }
}
