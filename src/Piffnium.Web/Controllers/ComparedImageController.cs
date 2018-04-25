using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Piffnium.Repository.Abstraction;

namespace Piffnium.Web.Controllers
{
    [Produces("image/jpeg")]
    [Route("compared")]
    public class ComparedImageController : Controller
    {
        private readonly IPiffniumRepositoryFactory repoFactory;
        private readonly IProcessRepository procRepo;

        public ComparedImageController(IPiffniumRepositoryFactory repoFactory)
        {
            this.repoFactory = repoFactory;
            this.procRepo = repoFactory.CreateProcessRepository();
        }


        [HttpGet("{processId}/{key}/expect")]
        public async Task<IActionResult> GetExpectImage(int processId, string key)
        {
            return File(await this.procRepo.GetExpectResult(processId, key), "image/jpeg");
        }

        [HttpGet("{processId}/{key}/actual")]
        public async Task<IActionResult> GetActualImage(int processId, string key)
        {
            return File(await this.procRepo.GetActualResult(processId, key), "image/jpeg");
        }


        [HttpGet("{processId}/{key}/diff")]
        public async Task<IActionResult> GetDiffImage(int processId, string key)
        {
            return File(await this.procRepo.GetDiffResult(processId, key), "image/jpeg");
        }

    }
}