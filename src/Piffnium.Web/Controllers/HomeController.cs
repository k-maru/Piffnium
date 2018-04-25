using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Piffnium.Repository.Abstraction;
using Piffnium.Web.Models;

namespace Piffnium.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPiffniumRepositoryFactory repoFactory;

        public HomeController(IPiffniumRepositoryFactory repoFactory)
        {
            this.repoFactory = repoFactory;
        }

        public async Task<IActionResult> Index()
        {
            var repo = this.repoFactory.CreateProcessRepository();
            var processes = await repo.GetAllProcessesAsync();

            return View(new IndexViewModel()
            {
                Processes = processes
            });
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
