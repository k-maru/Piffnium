using System;
using System.Collections.Generic;
using System.Text;

namespace Piffnium.Repository.Abstraction
{
    public interface IPiffniumRepositoryFactory
    {
        IProcessRepository CreateProcessRepository();

        IExpectRepository CreateExpectRepository();
    }
}
