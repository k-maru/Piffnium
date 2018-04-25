using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Piffnium.Repository.Abstraction;

namespace Piffnium.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/expect")]
    public class ExpectImageController : Controller
    {
        private readonly IPiffniumRepositoryFactory repoFactory;
        private readonly IProcessRepository procRepo;
        private readonly IExpectRepository expRepo;

        public ExpectImageController(IPiffniumRepositoryFactory repoFactory)
        {
            this.repoFactory = repoFactory;
            this.procRepo = repoFactory.CreateProcessRepository();
            this.expRepo = repoFactory.CreateExpectRepository();
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpectFromActual([FromForm]int processId, [FromForm]string key)
        {
            var imageStream = await this.procRepo.GetActualResult(processId, key);
            await this.expRepo.AddImageAsync(key, imageStream);

            return Created($"{processId}/{key}/expect", null);
        }
    }
}