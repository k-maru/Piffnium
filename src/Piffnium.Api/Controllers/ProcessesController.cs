using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Piffnium.Api.Model;
using Piffnium.Repository.Abstraction;

namespace Piffnium.Api.Controllers
{
    [Route("processes")]
    public class ProcessesController : Controller
    {
        private readonly IPiffniumRepositoryFactory repoFactory;

        public ProcessesController(IPiffniumRepositoryFactory repoFactory)
        {
            this.repoFactory = repoFactory;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProcessId()
        {
            var procRepo = this.repoFactory.CreateProcessRepository();
            var procId = await procRepo.CreateAsync();

            return Ok(new ProcessData()
            {
                Id = procId    
            });
        }
    }
}
