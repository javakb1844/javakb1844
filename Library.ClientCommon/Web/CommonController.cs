using Library.Core.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ClientCommon.Web
{
    public class CommonController : BaseController
    {
        public ServicesRepository Service { get; set; }
    }
}
