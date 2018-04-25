using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Piffnium.Repository.Abstraction
{
    public interface IProcessRepository
    {
        Task<int> CreateAsync();

        Task<bool> ExistsAsync(int processId);

        Task AddActualImageAsync(int processId, string key, Stream image);

        Task AddExpectImageAsync(int processId, string key, Stream image);

        Task AddDiffImageAsync(int processId, string key, Stream image);

        Task<IEnumerable<ProcessModel>> GetAllProcessesAsync();

        Task<IEnumerable<CompareResultItem>> GetAllResultsAsync(int processId);

        Task<Stream> GetExpectResult(int processId, string key);

        Task<Stream> GetActualResult(int processId, string key);

        Task<Stream> GetDiffResult(int processId, string key);
    }
}
