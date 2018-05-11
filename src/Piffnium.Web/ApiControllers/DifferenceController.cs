using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Piffnium.Comparator.Abstraction;
using Piffnium.Repository.Abstraction;
using Piffnium.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piffnium.Web.ApiControllers
{
    [Route("api/diff")]
    public class DifferenceController : Controller
    {
        private readonly IPiffniumRepositoryFactory repoFactory;
        private readonly IPictureComparatorFactory compFactory;

        public DifferenceController(IPiffniumRepositoryFactory repoFactory, IPictureComparatorFactory compFactory)
        {
            this.repoFactory = repoFactory;
            this.compFactory = compFactory;
        }

        [HttpPut]
        public async Task<IActionResult> CreateDifference([FromForm]int process, [FromForm]string key, [FromForm]IFormFile actual)
        {
            var processRepo = this.repoFactory.CreateProcessRepository();
            if (!await processRepo.ExistsAsync(process))
            {
                // TODO Error
                return BadRequest();
            }

            var expectRepo = this.repoFactory.CreateExpectRepository();
            var expectImage = expectRepo.GetImage(key);

            // 期待するファイルがない場合は、保存するだけ
            if (!expectImage.Exists)
            {
                using (var imageStream = actual.OpenReadStream())
                {
                    await processRepo.AddActualImageAsync(process, key, imageStream);
                }

                return Ok(new DifferenceResponseModel() { DifferenceRate = -1, Checked = false });
            }

            using (var actualStream = actual.OpenReadStream())
            using (var expectStream = expectImage.GetStream())
            {

                var comparator = compFactory.CreateComparator();
                var compResult = await comparator.CompareAsync(expectStream, actualStream);

                await processRepo.AddActualImageAsync(process, key, actualStream);
                await processRepo.AddExpectImageAsync(process, key, expectStream);
                await processRepo.AddDiffImageAsync(process, key, compResult.DiffImage);

                return Ok(new DifferenceResponseModel() { DifferenceRate = compResult.DifferenceRate, Checked = true });
            }
        }
    }
}
