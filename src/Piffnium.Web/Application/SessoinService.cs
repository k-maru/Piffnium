using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Piffnium.Comparator.Abstraction;
using Piffnium.Repository.Abstraction;
using Piffnium.Web.Domain.Entities;
using Piffnium.Web.Infrastructure.Configuration;
using Piffnium.Web.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Piffnium.Web.Application
{
    public class SessionService : ISessionService
    {
        private readonly PiffniumSettings settings;
        private readonly ApplicationDbContext dbContext;
        private readonly IPiffniumRepositoryFactory repoFactory;
        private readonly IPictureComparatorFactory comparatorFactory;

        public SessionService(ApplicationDbContext context, IOptions<PiffniumSettings> options, 
                              IPiffniumRepositoryFactory repoFactory, IPictureComparatorFactory compFactory)
        {
            this.settings = options.Value;
            this.dbContext = context;
            this.repoFactory = repoFactory;
            this.comparatorFactory = compFactory;
        }

        public async Task CreateComparionAsync(long sessionId, string comparisonKey, Stream actualImage)
        {
            var session = await this.dbContext.Sessions.Where(s => s.SessionId == sessionId)
                .AsNoTracking().FirstOrDefaultAsync().ConfigureAwait(false);

            if(session == null)
            {
                //Session Not Found;
                throw new InvalidOperationException();
            }

            var expectRepo = this.repoFactory.CreateExpectRepository();
            var expectImage = expectRepo.GetImage(comparisonKey);

            if (!expectImage.Exists)
            {
                //using (var imageStream = actual.OpenReadStream())
                //{
                //    await processRepo.AddActualImageAsync(sessionId, key, imageStream);
                //}
      

      //          return Ok(new DifferenceResponseModel() { DifferenceRate = -1, Checked = false });
            }

        }

        public async Task<long> StartNewSsessionAsync(string projectName)
        {
            var project = await this.dbContext.Projects.Where(p => p.ProjectName == projectName)
                .AsNoTracking().FirstOrDefaultAsync().ConfigureAwait(false);

            if(project == null)
            {
                if (!this.settings.ProjectAutoGeneration)
                {
                    //throw new ProjectNotFoundException(projectKey);
                    throw new Exception();
                }
                var projectEntity = await this.dbContext.Projects.AddAsync(new Project()
                {
                    ProjectName = projectName
                });
                await this.dbContext.SaveChangesAsync();
                project = projectEntity.Entity;
            }
            var sessionEntity = await this.dbContext.Sessions.AddAsync(new Session()
            {
                ProjectId = project.ProjectId,
                StartedAt = DateTime.Now
            });
            await this.dbContext.SaveChangesAsync();
            return sessionEntity.Entity.SessionId;
        }
    }
}
