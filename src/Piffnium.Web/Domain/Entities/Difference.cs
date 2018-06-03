using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piffnium.Web.Domain.Entities
{
    public class Difference
    {

        public long ComparisonId { get; set; }

        public double DifferenceRate { get; set; }

        public string SourceFileName { get; set; }

        public string DifferenceFileName { get; set; }

        public Comparison Comparison { get; set; }

    }
}
