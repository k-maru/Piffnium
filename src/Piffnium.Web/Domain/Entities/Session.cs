using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piffnium.Web.Domain.Entities
{
    public class Session
    {
        public long SessionId { get; set; }

        public long ProjectId { get; set; }

        public DateTime StartedAt { get; set; }

        public Project Project { get; set; }

        public ICollection<Comparison> Comparisons { get; private set; } = new List<Comparison>();

    }
}
