using Piffnium.Comparator.Abstraction;
using System;

namespace Piffnium.Comparator.ImageSharp
{
    public class PixelComparatorFactory : IPictureComparatorFactory
    {
        public IPictureComparator CreateComparator()
        {
            return new PixelComparator();
        }
    }
}
