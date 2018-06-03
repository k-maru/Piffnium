using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Tal.Mdm.Base
{
    public interface IQuerySpec<T>: IIncludeSpec<T>, ICriteriaSpec<T>
    {
    }
}
