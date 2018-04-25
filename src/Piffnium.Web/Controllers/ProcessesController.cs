using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Piffnium.Repository.Abstraction;

namespace Piffnium.Web.Controllers
{
    public class ProcessesController : Controller
    {
        private readonly IPiffniumRepositoryFactory repoFactory;

        public ProcessesController(IPiffniumRepositoryFactory repoFactory)
        {
            this.repoFactory = repoFactory;
        }


        [Route("[controller]/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var procRepo = this.repoFactory.CreateProcessRepository();
            var results = await procRepo.GetAllResultsAsync(id);
            return View(results);
        }
    }
}