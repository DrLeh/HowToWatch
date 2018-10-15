using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowToWatch.Mappers
{
    public abstract class BaseMapper<TModel, TView>
        where TModel : class, new()
        where TView : class, new()
    {
        public abstract TModel ToModel(TView view);
    }
}
