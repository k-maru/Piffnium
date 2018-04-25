using Piffnium.Comparator.Abstraction;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Piffnium.Comparator.ImageSharp
{
    public sealed class ImageSharpCompareResult : ICompareResult
    {
        private readonly Image<Rgba32> diffImage;

        public ImageSharpCompareResult(Image<Rgba32> diffImage, double differenceRate)
        {
            this.DifferenceRate = differenceRate;
            this.diffImage = diffImage;
        }

        public double DifferenceRate { get; }

        public Stream DiffImage
        {
            get
            {
                var stream = new MemoryStream();
                diffImage.SaveAsJpeg(stream);
                stream.Position = 0;
                return stream;
            }
        }
    }
}
