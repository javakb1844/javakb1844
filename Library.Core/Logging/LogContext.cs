using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Logging
{
    public partial class LogContext : DbContext
    {
        public string ConnectionString { get; set; }

        public LogContext()
            : base("name=LogContext")
        {

        }

        public LogContext(string connectionString)
            : base(connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public virtual DbSet<LogGroup> LogGroups { get; set; }
        public virtual DbSet<LogGroupFilter> LogGroupFilters { get; set; }
        public virtual DbSet<LogGroupDictionary> LogGroupDictionaries { get; set; }
        public virtual DbSet<LogItemType> LogItemTypes { get; set; }
        public virtual DbSet<LogDataType> LogDataTypes { get; set; }
        public virtual DbSet<LogItem> LogItems { get; set; }
        public virtual DbSet<LogItemDictionary> LogItemDictionaries { get; set; }

        /* Sample SP Calls */

        /*
        public virtual ObjectResult<InsertPropertyByXML2_Result> InsertPropertyByXML2()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<InsertPropertyByXML2_Result>("InsertPropertyByXML2");
        }

        public virtual int sp_DeleteProperty(string propertyNum)
        {
            var propertyNumParameter = propertyNum != null ?
                new ObjectParameter("propertyNum", propertyNum) :
                new ObjectParameter("propertyNum", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_DeleteProperty", propertyNumParameter);
        }*/

    }
}
