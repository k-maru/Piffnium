using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Piffnium.Repository.Abstraction
{
    public interface IProcessRepository
    {
        Task<int> CreateAsync();

        Task<bool> ExistsAsync(long processId);

        Task AddActualImageAsync(long processId, string key, Stream image);

        Task AddExpectImageAsync(long processId, string key, Stream image);

        Task AddDiffImageAsync(long processId, string key, Stream image);

        Task<IEnumerable<ProcessModel>> GetAllProcessesAsync();

        Task<IEnumerable<CompareResultItem>> GetAllResultsAsync(long processId);

        Task<Stream> GetExpectResult(long processId, string key);

        Task<Stream> GetActualResult(long processId, string key);

        Task<Stream> GetDiffResult(long processId, string key);
    }
}
