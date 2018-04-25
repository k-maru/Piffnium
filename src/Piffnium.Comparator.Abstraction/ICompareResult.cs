using System.IO;

namespace Piffnium.Comparator.Abstraction
{
    public interface ICompareResult
    {
        double  DifferenceRate { get; }

        Stream DiffImage { get; }
    }
}