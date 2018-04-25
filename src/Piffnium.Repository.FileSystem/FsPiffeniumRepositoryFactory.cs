using Piffnium.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Piffnium.Repository.FileSystem
{
    public sealed class FsPiffeniumRepositoryFactory : IPiffniumRepositoryFactory
    {
        private readonly FsRepositoryOptions options;

        public FsPiffeniumRepositoryFactory(FsRepositoryOptions options)
        {
            this.options = options;
        }

        public IExpectRepository CreateExpectRepository()
        {
            return new FsExpectRepository(options);
        }

        public IProcessRepository CreateProcessRepository()
        {
            return new FsProcessRepository(options);
        }
    }
}
