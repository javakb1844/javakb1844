using Data.HRMS.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Data.HRMS
{
    public class HRMSWorker
    {
        #region Public Methods
        public void ExecuteSql(string sql)
        {
            _db.Database.ExecuteSqlCommand(sql);
        }


        public void SaveChanges()
        {
            try
            {
                _db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                string strErrorMessage = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- HRMS: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                        strErrorMessage += " {'HRMSName': '" + ve.PropertyName + "', 'ErrorMessage': '" + ve.ErrorMessage + "'}";
                    }
                }
                throw new Exception(strErrorMessage);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public TransactionScope CreateTransaction()
        {
            return new TransactionScope();
        }

        public DbContext GetContext()
        {
            return _db;
        }
        public void Dispose()
        {
            if (_db != null)
                _db.Dispose();
        }

        #endregion

        #region Generic Repositories
        HRMSGenericRepository _Repository;
        public HRMSGenericRepository Repository
        {
            get
            {
                if (_Repository == null)
                {
                    _Repository = new HRMSGenericRepository(this._db);
                }
                return _Repository;
            }
            set
            {
                _Repository = value;
            }
        }

        #endregion

        #region Constructors

        public HRMSWorker(HRMSdb db)
        {
            SetDbContext(db);
        }

        public HRMSWorker(string connectionString, string adoConnectionString)
            : this(new HRMSdb(connectionString, adoConnectionString))
        {
        }

        public HRMSWorker()
        {
            string connectionString = "HRMSdb".Connectionstring();
            string adoConnectionstring = "HRMSdb_ADO".Connectionstring();

            if (String.IsNullOrEmpty(connectionString)
                || adoConnectionstring.IsNullOrEmpty())
            {
                throw new Exception("A connection string with name CRMDB or CRMDB_ADO is missing.");
            }

            SetDbContext(new HRMSdb(connectionString, adoConnectionstring));
        }

        void SetDbContext(HRMSdb context)
        {
            _db = context;
            _db.Configuration.AutoDetectChangesEnabled = false;
            //InitOperations();
        }

        #endregion


        private HRMSdb _db;
    }
}
