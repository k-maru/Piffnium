using Piffnium.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Piffnium.Repository.FileSystem
{
    public sealed class FsExpectImage : IExpectImage
    {
        public FsExpectImage(string imagePath)
        {
            this.ImagePath = imagePath;
        }

        public string ImagePath { get; }

        public bool Exists => File.Exists(this.ImagePath);

        public Stream GetStream()
        {
            if (!this.Exists) throw new InvalidOperationException();

            return File.Open(this.ImagePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        }
    }
}
