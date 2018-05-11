using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Piffnium.Repository.Abstraction;
using Piffnium.Web.Models;

namespace Piffnium.Web.ApiControllers
{
    [Route("api/processes")]
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

            return Ok(new ProcessResponseModel()
            {
                Id = procId
            });
        }
    }
}