using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Piffnium.Web.Application
{
    public interface ISessionService
    {
        Task<long> StartNewSsessionAsync(string projectKey);


        Task CreateComparionAsync(long sessionId, string comparisonKey, Stream actualImage);
    }
}
