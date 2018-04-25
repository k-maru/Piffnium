using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Piffnium.Repository.Abstraction;

namespace Piffnium.Web.Models
{
    public class IndexViewModel
    {
        public IEnumerable<ProcessModel> Processes { get; set; }
    }
}
