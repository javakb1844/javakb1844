using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.HRMS
{
    public partial class HRMSdb
    {
        public string AdoConnectionString { get; set; }

        public HRMSdb(string connectionString, string adoConnectionString)
            : base(connectionString)
        {
            var adapter = (IObjectContextAdapter)this;
            var objectContext = adapter.ObjectContext;
            objectContext.CommandTimeout = 120 * 60; // value in seconds

            AdoConnectionString = adoConnectionString;
        }

        public HRMSdb(bool explicitTimeout)
            : base("name=HRMSdb")
        {
            var adapter = (IObjectContextAdapter)this;
            var objectContext = adapter.ObjectContext;
            objectContext.CommandTimeout = 120 * 60; // value in seconds
            this.AdoConnectionString = "HRMSdb_ADO".Connectionstring();
        }
    }
}
