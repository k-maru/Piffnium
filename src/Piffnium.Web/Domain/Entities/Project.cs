using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piffnium.Web.Domain.Entities
{
    public class Project
    {
        public long ProjectId { get; set; }

        public string ProjectName { get; set; }

        public ICollection<Session> Sessions { get; private set; } = new List<Session>();

    }
}
