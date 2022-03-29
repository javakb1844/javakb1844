using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Objects;

namespace Library.Core.Data
{
    /// <summary>
    /// Generic Repository Default-Implementation
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity, TContext> : IRepository<TEntity>
        where TContext : DbContext
        where TEntity : class        
    {
        protected DbSet<TEntity> dbSet;
        protected TContext db;

        public Repository(DbContext dbContext)
        {
            db = dbContext as TContext;
            dbSet = db.Set<TEntity>();
        }

        #region IRepository<T> Members

        /// <summary>
        /// Generate & get next-Id-value for new-entity
        /// </summary>
        /// <returns></returns>
        public long GetNextId()
        {
            //string sql = "SELECT NEXT VALUE FOR dbo.Sequence_User_UserId";

            string tableName = GetT_TableName();
            string sequenceName = "Sequence_" + tableName + "_" + tableName + "Id";

            return GenerateNextId(sequenceName);
        }

        /// <summary>
        /// Add an entity to the EF - [without saving them to database immediately]
        /// </summary>
        /// <param name="entity"></param>
        public void Add(TEntity entity)
        {
            dbSet.Add(entity);

            //return DbSet.Add(entity);
            //var newEntry = db.Set<T>().Add(entity);
            //db.SaveChanges();
            //return newEntry;
        }

        /// <summary>
        /// Add list of entities to the EF - [without saving them to database immediately]
        /// </summary>
        /// <param name="entities"></param>
        public void Add(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                this.Add(entity);

            //{
            //     db.Set<T>().Add(entity);
            //}
            //db.SaveChanges();
        }

        /// <summary>
        /// Remove an entity from EF - [without saving them to database immediately]
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        /// <summary>
        /// Update an entity in EF - [without saving them to database immediately]
        /// </summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity)
        {
            var entry = db.Entry(entity);
            db.Set<TEntity>().Attach(entity);
            entry.State = EntityState.Modified;



            //dbSet.Attach(entity);

            //db.SaveChanges();            
        }


        #region Search Entities

        /// <summary>
        /// Find entity by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetById(long id)
        {
            return dbSet.Find(id);
        }

        //public T GetById(int id)
        //{
        //    return dbSet.Find(id);
        //}

        public TEntity Get(Expression<Func<TEntity, bool>> filter = null, string includingProperties = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
             
            }

            if (includingProperties != null)
            {
                foreach (var includeproperty in includingProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeproperty);
                }
            }

             return query.AsQueryable().FirstOrDefault();
        }

        public int Count()
        {
            return dbSet.Count();
        }

        public int Count(Expression<Func<TEntity, bool>> predeciate)
        {
            return dbSet.Count(predeciate);
        }
        
        /// <summary>
        /// Search entities
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Search(Expression<Func<TEntity, bool>> wherePredicate)
        {
            return dbSet.Where(wherePredicate);
        }

        /// <summary>
        /// Get All entities in database
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetAll()
        {
            return dbSet;
        }
        
        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderyby = null, string includingProperties = null)
        {

            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
             
            }


            if (includingProperties != null)
            {
                foreach (var includeproperty in includingProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeproperty);
                }
            }
            if (orderyby != null)
            {
                return orderyby(query).AsQueryable();
            }
            else
            {
                return query.AsQueryable();
            }
        }

        /// <summary>
        /// Get DbSet reference to write your custom LINQ 
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetDbSet()
        {
            return dbSet;
        }


        #endregion

        #region Paging - Parameterized

        /// <summary>
        /// Get First-Page data (0-first page)
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetFirstPage()
        {
            return dbSet.Take(() => pageSize);

            // Hint: Below lines are not parameterized by EF
            // ---------------------------------------------
            //return dbSet.Take(10);
            //return dbSet.Take(pageSize);
        }
        public IQueryable<TEntity> GetFirstPage(string sorPropertyName, bool ascending = true)
        {
            return OrderByFieldForPaging(sorPropertyName, ascending).Take(() => pageSize);
            // Hint: Below lines are not parameterized by EF
            // ---------------------------------------------
            //return dbSet.Take(10);
            //return dbSet.Take(pageSize);
        }


      



        /// <summary>
        /// Get Next-Page data
        /// </summary>
        /// <param name="currentPageNo"></param>
        /// <param name="newPageNo"></param>
        /// <returns></returns>
        public IQueryable<TEntity> GetNextPage(int currentPageNo, out int newPageNo)
        {
            int skips = util_GetSkipsForThisPage(currentPageNo + 1, out newPageNo);
            return OrderByFieldForPaging().Skip(() => skips).Take(() => pageSize);

            //return dbSet.OrderBy(...).Skip(() => skips).Take(() => pageSize);
        }
        public IQueryable<TEntity> GetNextPage(int currentPageNo, out int newPageNo, string sorPropertyName, bool ascending = true)
        {
            int skips = util_GetSkipsForThisPage(currentPageNo + 1, out newPageNo);
            return OrderByFieldForPaging(sorPropertyName, ascending).Skip(() => skips).Take(() => pageSize);

            //return dbSet.OrderBy(...).Skip(() => skips).Take(() => pageSize);
        }



        public IQueryable<TEntity> GetNextPage(int currentPageNo, out int newPageNo, string includingProperties = null)
        {
            int skips = util_GetSkipsForThisPage(currentPageNo + 1, out newPageNo);
            return OrderByFieldForPaging().Skip(() => skips).Take(() => pageSize);

            //return dbSet.OrderBy(...).Skip(() => skips).Take(() => pageSize);
        }

        /// <summary>
        /// Get Previous-Page data
        /// </summary>
        /// <param name="currentPageNo"></param>
        /// <param name="newPageNo"></param>
        /// <returns></returns>
        public IQueryable<TEntity> GetPreviousPage(int currentPageNo, out int newPageNo)
        {
            int skips = util_GetSkipsForThisPage(currentPageNo - 1, out newPageNo);
            return OrderByFieldForPaging().Skip(() => skips).Take(() => pageSize);

            //return dbSet.OrderBy(...).Skip(() => skips).Take(() => pageSize);
        }
        public IQueryable<TEntity> GetPreviousPage(int currentPageNo, out int newPageNo, string sorPropertyName, bool ascending = true)
        {
            int skips = util_GetSkipsForThisPage(currentPageNo - 1, out newPageNo);
            return OrderByFieldForPaging(sorPropertyName, ascending).Skip(() => skips).Take(() => pageSize);

            //return dbSet.OrderBy(...).Skip(() => skips).Take(() => pageSize);
        }

        /// <summary>
        /// Get Last-Page data
        /// </summary>
        /// <param name="lastPageNo"></param>
        /// <returns></returns>
        public IQueryable<TEntity> GetLastPage(out int lastPageNo)
        {
            int skips = util_GetSkipsForLastPage(out lastPageNo);
            return OrderByFieldForPaging().Skip(() => skips).Take(() => pageSize);

            //return dbSet.OrderBy(...).Skip(() => skips).Take(() => pageSize);
        }
        public IQueryable<TEntity> GetLastPage(out int lastPageNo, string sorPropertyName, bool ascending = true)
        {
            int skips = util_GetSkipsForLastPage(out lastPageNo);
            return OrderByFieldForPaging(sorPropertyName, ascending).Skip(() => skips).Take(() => pageSize);

            //return dbSet.OrderBy(...).Skip(() => skips).Take(() => pageSize);
        }
        // TODO: Get from database app-preferences
        int pageSize = 10;

        #endregion

        #region Batch Operations

        /// <summary>
        /// Batch Delete operation - [executed immediately to database]     Example: (u => u.UserName == null )
        /// </summary>
        /// <param name="wherePredicate"></param>
        /// <returns></returns>
        public int BatchDelete(Expression<Func<TEntity, bool>> wherePredicate)
        {
            return dbSet.Where(wherePredicate).Delete();
        }

        /// <summary>
        /// Batch Delete operation, by providing list of Ids - [executed immediately to database]
        /// </summary>
        /// <param name="ids"></param>
        public void BatchDelete(List<long> ids)
        {
            string tableName = GetT_TableName();
            string primaryKey = GetT_TableKeyColumnName();

            var totalCount = ids.Count;
            if (totalCount == 0)
            {
                return;
            }

            var chunks = CreateChunksOfIds(ids, 2000);

            foreach (var key in chunks.Keys)
            {
                var thisChunk = chunks[key];
                var countChunk = thisChunk.Count;
                var sb = new StringBuilder();
                sb.Append("Delete from [");
                sb.Append(tableName);
                sb.Append("] Where ");
                sb.Append(primaryKey);
                sb.Append(" IN (");
                for (int i = 0; i < countChunk; i++)
                {
                    if (i > 0)
                    {
                        sb.Append(",");
                    }
                    sb.Append(thisChunk[i]);
                }

                sb.Append(")");

                db.Database.ExecuteSqlCommand(sb.ToString());
            }
        }

        /// <summary>
        /// Batch Update operation - [executed immediately to database]
        /// Example: (u => u.EmailAddress.EndsWith("@insight.com.au"),             new User { EmailAddress = "@my-insight.com.au" } )
        /// </summary>
        /// <param name="wherePredicate"></param>
        /// <param name="updateExpression"></param>
        /// <returns></returns>
        public int BatchUpdate(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, TEntity>> updateExpression)
        {
            return dbSet.Where(wherePredicate).Update(updateExpression);
        }

        /// <summary>
        /// Batch Insert operation, by providing list-of-entities - [executed immediately to database]
        /// STATUS: in-progress     - initially entity.properties will be supported, later child entities like User.UserDetail will be supported to n-nested-level
        /// </summary>
        /// <param name="entities"></param>
        void BatchInsert_InProgress(List<TEntity> entities)
        {
            if (entities == null || entities.Count <= 0)
                return;

            var columns = GetT_TableColumnsList();

            StringBuilder sb = new StringBuilder();
            List<string> sqlChunks = new List<string>();
            int chunkLengthLimit = 2000; //TODO: from 

            foreach (var entity in entities)
            {
                if (sb.Length <= 0)
                {
                    sb.Append("INSERT INTO (col1, col2...)");

                    foreach (var col in columns)
                    {

                    }


                    sb.Append(") ");
                }

                //if(sb.Length <= 0 || sb.Length >= )
                // 1. create INSERT INTO (col1, col2, ...)
                // 2. create SELECT col1Value, col2Value, ...
                // 3. UNION ALL - if next row exists


            }
        }

        #endregion

        #endregion

        #region Utility Methods

        /// <summary>
        /// TODO: 1. Change return type int to long 
        /// 2. In Database set Ids to bingint instead int
        /// </summary>
        /// <param name="sequenceName">name of sequence</param>
        /// <returns></returns>
        protected long GenerateNextId(string sequenceName)
        {
            string sql = "SELECT NEXT VALUE FOR dbo." + sequenceName;
            var id = dbSet.GetContext().ExecuteStoreQuery<long>(sql).First();
            return id;// (int)id;

            // NOTE:    Always use "long or int" not "mixed" throughout domain databases, otherwise, exception will be raised 
            //          Exception: "The specified cast from a materialized 'System.Int32' type to the 'System.Int64' type is not valid."

            // Issue: "The underlying provider failed on Open." 
            // Article: http://stackoverflow.com/questions/2475008/the-underlying-provider-failed-on-open
            // Guess: potential reason is MSDTC not installed
            // Resolved: by running MSDTC service on windows-7


            //string sql = "SELECT NEXT VALUE FOR dbo.Sequence_User_UserId";
            //string tableName = GetTableName(typeof(T), db);
            //string sql = "SELECT NEXT VALUE FOR dbo.Sequence_" + tableName + "_" + tableName + "Id";
        }

        int util_GetSkipsForFirstPage()
        {
            return 0;
        }

        int util_GetSkipsForThisPage(int pageNo, out int newPageNo)
        {
            newPageNo = pageNo;

            if (pageNo <= 0)
                return util_GetSkipsForFirstPage();

            int allPagesCount = util_GetTotalPagesCount();

            if (pageNo > allPagesCount)
                return util_GetSkipsForLastPage(out newPageNo, allPagesCount);

            return pageNo * pageSize;

            //return (pageNo - 1) * pageSize;
        }

        int util_GetSkipsForLastPage(out int newPageNo, int allPagesCount = -1)
        {
            newPageNo = allPagesCount;

            if (allPagesCount < 0)
                allPagesCount = util_GetTotalPagesCount();

            newPageNo = allPagesCount;
            if (allPagesCount <= 0)
                return 0;

            // 101 = 11 -> (11 - 1) * 10 = 100 skips
            // 100 = 10 -> (10 - 1) * 10 = 90 skips
            return (allPagesCount - 1) * pageSize;
        }

        int util_GetTotalPagesCount()
        {
            int totalCount = util_GetAllCount();

            // min pageSize = 10, max pageSize = 1000;
            if (pageSize < 10)
                pageSize = 10;

            if (pageSize > 1000)
                pageSize = 1000;

            // 0/10 = 0, 100/10 = 10, 101/10 = 10+1
            int pagesCount = (totalCount > 0) ? totalCount / pageSize : 0;  // +1; // 0/10 = 0 + 1

            if (totalCount > pageSize && totalCount % pageSize > 0)
                pagesCount++;

            return pagesCount;
        }

        int util_GetAllCount()
        {
            return dbSet.Count();
        }

        // Issue: "The method 'Skip' is only supported for sorted input in LINQ to Entities. The method 'OrderBy' must be called before the method 'Skip'."



        // http://romiller.com/2014/04/08/ef6-1-mapping-between-types-tables/
        public string GetT_TableName() //Type type, DbContext context)
        {
            var table = GetT_Table_EntitySet(typeof(TEntity), db);
            return (string)table.MetadataProperties["Table"].Value ?? table.Name;
        }
        protected string GetT_TableKeyColumnName() //Type type, DbContext context)
        {
            var table = GetT_Table_EntitySet(typeof(TEntity), db);

            if (table.ElementType.KeyMembers.Count > 0)
                return table.ElementType.KeyMembers[0].Name;

            return "";
        }
        List<EntityTableColumn> GetT_TableColumnsList()
        {
            List<EntityTableColumn> columns = new List<EntityTableColumn>();
            var table = GetT_Table_EntitySet(typeof(TEntity), db);

            foreach (var p in table.ElementType.Properties)
                columns.Add(new EntityTableColumn { Name = p.Name, ClrFullName = p.PrimitiveType.ClrEquivalentType.FullName });

            return columns;
        }
        static EntitySet GetT_Table_EntitySet(Type type, DbContext context)
        {
            var metadata = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;

            // Get the part of the model that contains info about the actual CLR types
            var objectItemCollection = ((ObjectItemCollection)metadata.GetItemCollection(DataSpace.OSpace));

            // Get the entity type from the model that maps to the CLR type
            var entityType = metadata
                    .GetItems<EntityType>(DataSpace.OSpace)
                    .Single(e => objectItemCollection.GetClrType(e) == type);

            // Get the entity set that uses this entity type
            var entitySet = metadata
                .GetItems<EntityContainer>(DataSpace.CSpace)
                .Single()
                .EntitySets
                .Single(s => s.ElementType.Name == entityType.Name);

            // Find the mapping between conceptual and storage model for this entity set
            var mapping = metadata.GetItems<EntityContainerMapping>(DataSpace.CSSpace)
                    .Single()
                    .EntitySetMappings
                    .Single(s => s.EntitySet == entitySet);

            // Find the storage entity set (table) that the entity is mapped
            var table = mapping
                .EntityTypeMappings.Single()
                .Fragments.Single()
                .StoreEntitySet;

            return table;
            // Return the table name from the storage entity set
            //return (string)table.MetadataProperties["Table"].Value ?? table.Name;
        }

        // TODO - change to extension method
        IQueryable<TEntity> OrderByFieldForPaging()
        {
            IQueryable<TEntity> q = dbSet;
            string SortField = GetT_TableKeyColumnName();
            bool Ascending = true;

            var param = Expression.Parameter(typeof(TEntity), "p");
            var prop = Expression.Property(param, SortField);
            var exp = Expression.Lambda(prop, param);
            string method = Ascending ? "OrderBy" : "OrderByDescending";
            Type[] types = new Type[] { q.ElementType, exp.Body.Type };
            var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
            return q.Provider.CreateQuery<TEntity>(mce);
        }
        IQueryable<TEntity> OrderByFieldForPaging(string propertyName, bool ascending)
        {
            IQueryable<TEntity> q = dbSet;
            string SortField = (string.IsNullOrEmpty(propertyName)) ? GetT_TableKeyColumnName() : propertyName;
            //bool Ascending = true;

            var param = Expression.Parameter(typeof(TEntity), "p");
            var prop = Expression.Property(param, SortField);
            var exp = Expression.Lambda(prop, param);
            string method = ascending ? "OrderBy" : "OrderByDescending";
            Type[] types = new Type[] { q.ElementType, exp.Body.Type };
            var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
            return q.Provider.CreateQuery<TEntity>(mce);
        }







        // TODO: copied from IM. BulkHelper extensions
        Dictionary<int, List<int>> CreateChunksOfIds(List<int> ids, int chunkSize)
        {
            var result = new Dictionary<int, List<int>>();
            var count = ids.Count();
            var totalChunks = Math.Ceiling((double)count / (double)chunkSize);
            var remainder = count % chunkSize;

            //if not need to iterate 
            if (totalChunks < 2)
            {
                result.Add(0, ids.ToList());
                return result;
            }

            var countFullChunks = totalChunks - 1;
            var lastStartTake = 0;
            for (int i = 0; i < countFullChunks; i++)
            {
                var startTake = (i * chunkSize); // 0, 100, 200
                var endTake = ((i + 1) * chunkSize) - 1; // 99, 199, 299
                var fullList = new List<int>();
                for (int i2 = startTake; i2 <= endTake; i2++)
                {
                    fullList.Add(ids[i2]);
                }
                result.Add(i, fullList);
                lastStartTake = endTake + 1;
            }

            // Last chunk
            var lastEndTake = lastStartTake + (remainder - 1);
            var partialList = new List<int>();
            for (int i2 = lastStartTake; i2 <= lastEndTake; i2++)
            {
                partialList.Add(ids[i2]);
            }
            result.Add((int)countFullChunks, partialList);

            return result;
        }
        Dictionary<int, List<long>> CreateChunksOfIds(List<long> ids, int chunkSize)
        {
            var result = new Dictionary<int, List<long>>();
            var count = ids.Count();
            var totalChunks = Math.Ceiling((double)count / (double)chunkSize);
            var remainder = count % chunkSize;

            //if not need to iterate 
            if (totalChunks < 2)
            {
                result.Add(0, ids.ToList());
                return result;
            }

            var countFullChunks = totalChunks - 1;
            var lastStartTake = 0;
            for (int i = 0; i < countFullChunks; i++)
            {
                var startTake = (i * chunkSize); // 0, 100, 200
                var endTake = ((i + 1) * chunkSize) - 1; // 99, 199, 299
                var fullList = new List<long>();
                for (int i2 = startTake; i2 <= endTake; i2++)
                {
                    fullList.Add(ids[i2]);
                }
                result.Add(i, fullList);
                lastStartTake = endTake + 1;
            }

            // Last chunk
            var lastEndTake = lastStartTake + (remainder - 1);
            var partialList = new List<long>();
            for (int i2 = lastStartTake; i2 <= lastEndTake; i2++)
            {
                partialList.Add(ids[i2]);
            }
            result.Add((int)countFullChunks, partialList);

            return result;
        }

        #endregion

        #region Utility Methods - Batch Operations

        void util_AddEntityPropertyValuesSQL(TEntity entity, List<EntityTableColumn> columns, StringBuilder sb)
        {
            // reading entity property values
            // http://msdn.microsoft.com/en-us/data/jj592677.aspx

            if (columns == null || columns.Count <= 0)
                return;

            for (int i = 0; i < columns.Count; i++)
            {
                var col = columns[i];

                // if numeric, string, or date etc. etc.

                sb.Append("[" + col.Name + "]");

                if (i < columns.Count - 1)
                    sb.Append(", ");
            }

            //            db.Entry(entity).Property("")


        }
        void util_AddEntityColumnsSQL(List<EntityTableColumn> columns, StringBuilder sb)
        {
            if (columns == null && columns.Count <= 0)
                return;

            for (int i = 0; i < columns.Count; i++) //(var col in columns)
            {
                var col = columns[i];

                sb.Append("[" + col.Name + "]");

                if (i < columns.Count - 1)
                    sb.Append(", ");
            }
        }

        #endregion

        class EntityTableColumn
        {
            public string Name { get; set; }
            public string ClrFullName { get; set; }
        }

        public ObjectContext GetObjectContext()
        {
            return ((IObjectContextAdapter)db).ObjectContext;
        }
    }
}
