using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Core.Data;

namespace Data.HRMS.Repository
{
    public class HRMSGenericRepository : GenericRepository<HRMSdb>
    {
        public HRMSGenericRepository(System.Data.Entity.DbContext dbContext)
            : base(dbContext)
        {

        }
       
    }
}
