using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Piffnium.Repository.Abstraction
{
    public interface IExpectRepository
    {
        IExpectImage GetImage(string key);
        Task AddImageAsync(string key, Stream imageStream);
    }
}
