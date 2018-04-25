using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Piffnium.Comparator.Abstraction
{
    public interface IPictureComparator
    {
        Task<ICompareResult> CompareAsync(Stream source, Stream dest);
    }
}
