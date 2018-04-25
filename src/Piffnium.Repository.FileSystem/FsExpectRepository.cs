using Piffnium.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Piffnium.Repository.FileSystem
{
    public sealed class FsExpectRepository : IExpectRepository
    {
        private const string ExpectDirectoryName = "expects";
        private readonly string expectDirectory;
        private readonly FsRepositoryOptions options;

        public FsExpectRepository(FsRepositoryOptions options)
        {
            this.options = options;
            this.expectDirectory = GetAndPrepareExpectDirectory();
        }

        private string GetAndPrepareExpectDirectory()
        {
            var expectDir = Path.Combine(this.options.RootDirectory, ExpectDirectoryName);
            if (!Directory.Exists(expectDir))
            {
                Directory.CreateDirectory(expectDir);
            }
            return expectDir;
        }

        public IExpectImage GetImage(string key)
        {
            return new FsExpectImage(Path.Combine(this.expectDirectory, $"{key}.jpeg"));
        }

        public async Task AddImageAsync(string key, Stream imageStream)
        {
            var targetFileName = Path.Combine(this.expectDirectory, $"{key}.jpeg");
            var bytes = await ToByteArrayAsync(imageStream);
            await File.WriteAllBytesAsync(targetFileName, bytes);
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
    }
}
