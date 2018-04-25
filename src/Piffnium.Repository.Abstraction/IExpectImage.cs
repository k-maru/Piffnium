using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Piffnium.Repository.Abstraction
{
    public interface IExpectImage
    {
        Stream GetStream();

        bool Exists { get; }
    }
}
