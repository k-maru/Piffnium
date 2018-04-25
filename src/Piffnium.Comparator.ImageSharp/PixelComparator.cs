using Piffnium.Comparator.Abstraction;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Piffnium.Comparator.ImageSharp
{
    public class PixelComparator : IPictureComparator
    {
        public async Task<ICompareResult> CompareAsync(Stream source, Stream dest)
        {
            return await Task.Run(() =>
            {
                var sourceImage = Image.Load<Rgba32>(source);
                var destImage = Image.Load<Rgba32>(dest);
                var (diffImage, differanceRate) = CheckDiff(sourceImage, destImage);
                return new ImageSharpCompareResult(diffImage, differanceRate);
            });
        }

        private (Image<Rgba32> diffImage, double differanceRate) CheckDiff(Image<Rgba32> image1, Image<Rgba32> image2)
        {
            var width = Math.Max(image1.Width, image2.Width);
            var height = Math.Max(image1.Height, image2.Height);
            var newImage = new Image<Rgba32>(width, height);
            var differenceCount = 0;
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    if (image1.Width <= i || image1.Height <= j || image2.Width <= i || image2.Height <= j)
                    {
                        newImage[i, j] = new Rgba32(255, 96, 96);
                        differenceCount++;
                    }
                    else
                    {
                        var px1 = image1[i, j];
                        var px2 = image2[i, j];
                        if (px1.Rgba != px2.Rgba)
                        {
                            newImage[i, j] = new Rgba32(255, 96, 96);
                            differenceCount++;
                        }
                        else
                        {

                            var val = (byte)((0.3 * (double)px1.R) + (0.59 * (double)px1.G) + (0.11 * (double)px1.B));
                            var newPx = new Rgba32(val,val,val, px1.A);
                            
                            newImage[i, j] = newPx;
                        }
                    }
                }
            }
            return (newImage, differenceCount == 0 ? 0.0 : (double)differenceCount / ((double)width * (double)height));
        }
    }
}
