using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Services
{
    public interface IMapper<TSource, TTarget>
    {
        TTarget MapDataModelToVm(TSource source);
        TSource MapVmToDataModel(TTarget source);
    }
}
