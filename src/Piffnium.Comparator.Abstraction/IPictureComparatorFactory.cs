using System;

namespace Piffnium.Comparator.Abstraction
{
    public interface IPictureComparatorFactory
    {
        IPictureComparator CreateComparator();
    }
}
