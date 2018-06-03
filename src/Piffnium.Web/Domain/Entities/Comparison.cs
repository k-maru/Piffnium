using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piffnium.Web.Domain.Entities
{
    public class Comparison
    {
        public long ComparisonId { get; set; }

        public string ComparisonKey { get; set; }

        public long SessionId { get; set; }

        public string DestinationFileName { get; set; }

        public DateTime ComparedAt { get; set; }

        public Session Session { get; set; }

        public Difference Difference { get; set; }

    }
}
