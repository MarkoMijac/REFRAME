using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.FluentAPI
{
    public static class GraphExtension
    {
        public static TransferObject Let(this IDependencyGraph instance, params Expression<Func<object>>[] expressions)
        {
            if (instance == null)
            {
                throw new FluentException("Dependency graph cannot be null!");
            }
            

            return new TransferObject();
        }
    }
}
