using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Piffnium.Repository.Abstraction;
using Piffnium.Web.Application;
using Piffnium.Web.Models;

namespace Piffnium.Web.ApiControllers
{
    [Route("api/sessions")]
    public class ProcessesController : Controller
    {
        private readonly IPiffniumRepositoryFactory repoFactory;
        private readonly ISessionService sessionService;

        public ProcessesController(ISessionService sessionService, IPiffniumRepositoryFactory repoFactory)
        {
            this.repoFactory = repoFactory;
            this.sessionService = sessionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProcessId([FromQuery(Name = "pn")]string projectName)
        {
            var sessionId = await this.sessionService.StartNewSsessionAsync(projectName);
            return Ok(new ProcessResponseModel()
            {
                Id = sessionId
            });

            //var procRepo = this.repoFactory.CreateProcessRepository();
            //var procId = await procRepo.CreateAsync();

            
        }
    }
}