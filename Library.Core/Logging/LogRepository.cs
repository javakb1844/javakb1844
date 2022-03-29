using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Logging
{
    public class LogRepository 
    {
        public LogRepository(string connectionString)
        {
            this.DB = new LogContext(connectionString);            
        }

        public LogContext DB
        {
            get;
            set;
        }

        public List<LogItemType> GetLogItemTypes()
        {
            return DB.LogItemTypes.ToListSafely();
        }

        public List<LogDataType> GetLogDataTypes()
        {
            return DB.LogDataTypes.ToListSafely();
        }

        public void SaveLogGroup(LogGroup group)
        {
            DB.LogGroups.Add(group);
        }

        public List<LogGroup> SearchLogs(Dictionary<string, string> parameters)
        {
            throw new NotImplementedException();
        }

        public List<LogGroup> SearchLogs(Dictionary<string, string> parameters, int skip, int take)
        {
            throw new NotImplementedException();
        }

        public void ExpireLogs(DateTime expiryDate)
        {
            throw new NotImplementedException();
        }

        
    }
}
