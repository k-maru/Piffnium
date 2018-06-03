using Piffnium.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Piffnium.Repository.FileSystem
{
    public sealed class FsProcessRepository : IProcessRepository
    {
        private readonly FsRepositoryOptions options;
        private const string MetadataFileName = "metadata.txt";
        private const string ActualImageSuffix = "actual";
        private const string ExpectImageSuffix = "expect";
        private const string DiffImageSuffix = "diff";
        private const string FileExtension = ".jpeg";

        public FsProcessRepository(FsRepositoryOptions options)
        {
            this.options = options;
        }

        public async Task<int> CreateAsync()
        {
            var dir = new DirectoryInfo(this.options.RootDirectory);
            var names = dir.GetDirectories().Select(di => di.Name).ToList();
            var startid = names.Count();

            while (true)
            {
                if (!names.Contains(startid.ToString()))
                {
                    await CreateProcessDirectoryAndMetadata(startid);
                    return startid;
                }
                startid++;
            }
        }

        private async Task CreateProcessDirectoryAndMetadata(long processId)
        {
            var di = Directory.CreateDirectory(this.options.GetDirectoryNameWithRoot(processId));
            using (var sw = new StreamWriter(Path.Combine(di.FullName, MetadataFileName)))
            {
                sw.WriteLine(DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss.fff"));
                await sw.FlushAsync();
            }
        }

        public async Task<bool> ExistsAsync(long processId)
        {
            return await Task.Run<bool>(() =>
            {
                return Directory.Exists(this.options.GetDirectoryNameWithRoot(processId));
            });
        }

        public async Task AddActualImageAsync(long processId, string key, Stream image)
        {
            await AddImageAsync(processId, key, ActualImageSuffix, image);
        }

        public async Task AddExpectImageAsync(long processId, string key, Stream image)
        {
            await AddImageAsync(processId, key, ExpectImageSuffix, image);
        }

        public async Task AddDiffImageAsync(long processId, string key, Stream image)
        {
            await AddImageAsync(processId, key, DiffImageSuffix, image);
        }

        private async Task AddImageAsync(long processId, string key, string suffix, Stream image)
        {
            var fileName = $"{key}_{suffix}{FileExtension}";
            var fullName = Path.Combine(this.options.GetDirectoryNameWithRoot(processId), fileName);

            await File.WriteAllBytesAsync(fullName, await ToByteArrayAsync(image));
        }


        private async Task<byte[]> ToByteArrayAsync(Stream stream)
        {
            stream.Position = 0;
            using (var ms = new MemoryStream())
            {
                await stream.CopyToAsync(ms);
                return ms.ToArray();
            }
        }

        public async Task<IEnumerable<ProcessModel>> GetAllProcessesAsync()
        {
            return await Task.Run(() =>
            {
                var dir = new DirectoryInfo(this.options.RootDirectory);
                return dir.GetDirectories().Where(di =>
                {
                    return int.TryParse(di.Name, out int val);
                }).Select(num =>
                {
                    return new ProcessModel() { Id = int.Parse(num.Name) };
                });
            });
        }

        public async Task<IEnumerable<CompareResultItem>> GetAllResultsAsync(long processId)
        {
            return await Task.Run(() =>
            {
                return Directory.GetFiles(this.options.GetDirectoryNameWithRoot(processId))
                .Where(fn => fn.EndsWith(FileExtension))
                .Select(fn => Path.GetFileName(fn))
                .Select(fn =>
                {
                    var keyAndTypeFileName = fn.Split('_');
                    (string key, string category) result = (keyAndTypeFileName[0], keyAndTypeFileName[1]);
                    return result;
                }).GroupBy(fn => fn.key)
                .Select(g =>
                {
                    return new CompareResultItem()
                    {
                        ProcessId = processId,
                        Key = g.Key,
                        HasExpect = g.Any(v => v.category == $"{ExpectImageSuffix}{FileExtension}"),
                        HasActual = g.Any(v => v.category == $"{ActualImageSuffix}{FileExtension}"),
                        HasDiff = g.Any(v => v.category == $"{DiffImageSuffix}{FileExtension}")
                    };
                });
            });
        }

        public async Task<Stream> GetExpectResult(long processId, string key)
        {
            return await GetResultItem(processId, key, ExpectImageSuffix);
        }

        public async Task<Stream> GetActualResult(long processId, string key)
        {
            return await GetResultItem(processId, key, ActualImageSuffix);
        }

        public async Task<Stream> GetDiffResult(long processId, string key)
        {
            return await GetResultItem(processId, key, DiffImageSuffix);
        }

        private async Task<Stream> GetResultItem(long processId, string key, string suffix)
        {
            return await Task.Run(() =>
            {
                return new MemoryStream(File.ReadAllBytes(Path.Combine(this.options.GetDirectoryNameWithRoot(processId), $"{key}_{suffix}{FileExtension}")));
            });
        }
    }
}
