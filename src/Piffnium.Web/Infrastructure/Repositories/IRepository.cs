using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tal.Mdm.Base;

namespace Piffnium.Web.Infrastructure.Repositories
{
    public interface IRepository<T>
    {
        Task<T> SingleAsync(IQuerySpec<T> spec);

        Task<IEnumerable<T>> ListAsync(IQuerySpec<T> spec);
    }
}
