using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
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
    public class GenericRepository<TContext>
        where TContext : DbContext
    {
        public TContext db;

        public GenericRepository(DbContext dbContext)
        {
            db = dbContext as TContext;
        }

        public void Create<TEntity>(TEntity entity)
            where TEntity : class
        {
            db.Set<TEntity>().Add(entity);
        }

        public void Create<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class
        {
            entities.ToListSafely().ForEach(e => db.Set<TEntity>().Add(e));
        }

        public IQueryable<TEntity> Read<TEntity>()
            where TEntity : class
        {
            return db.Set<TEntity>().AsQueryable<TEntity>();
        }

        public IQueryable<TEntity> Read<TEntity>(string includingProperties)
            where TEntity : class
        {
            IQueryable<TEntity> query = db.Set<TEntity>();

            if (includingProperties != null)
            {
                foreach (var includeproperty in includingProperties.Split
                    (new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeproperty);
                }
            }
                
            return query.AsQueryable();
        }
        

        public void Update<TEntity>(TEntity entity)
            where TEntity : class
        {
            var entry = db.Entry<TEntity>(entity);
            db.Set<TEntity>().Attach(entity);
            entry.State = EntityState.Modified;
        }

       
        public void Update<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class
        {
            entities.ToListSafely().ForEach(entity => 
            {
                var entry = db.Entry<TEntity>(entity);
                db.Set<TEntity>().Attach(entity);
                entry.State = EntityState.Modified;
            });
        }
        public bool Update<T>(T item, params string[] changedPropertyNames) 
            where T : class
        {
            var entry = db.Entry<T>(item);               
            db.Set<T>().Attach(item);            //entry.State = EntityState.Modified;  
                                 
            foreach (var propertyName in changedPropertyNames)
            {
                // If we can't find the property, this line will throw an exception, 
                //which is good as we want to know about it

                db.Entry(item).Property(propertyName).IsModified = true;
               
            }
            // No need to explicitly save changes
            db.SaveChanges();
            return true;
        }


        /// <summary>
        /// update object and will not update the fields that you pass it as fields paramters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="fields">comman seperated fileds that you don't want to update.</param>
        public void UpdateExcludingPassingProperties<T>(T item, List<string> fields)
          where T : class
        {
            var entry = db.Entry<T>(item);
            db.Set<T>().Attach(item);            

            foreach (var propertyName in fields)
            {
                db.Entry(item).Property(propertyName).IsModified = false;
            }

        }

        

        public void Delete<TEntity>(TEntity entity)
            where TEntity : class
        {
            db.Set<TEntity>().Remove(entity);
        }

        public void Delete<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class
        {
            entities.ToListSafely().ForEach(e => db.Set<TEntity>().Remove(e));
        }

        public long GetNextId<TEntity>()
            where TEntity : class
        {
            //string sql = "SELECT NEXT VALUE FOR dbo.Sequence_User_UserId";

            string tableName = GetT_TableName<TEntity>();
            string sequenceName = "Sequence_" + tableName + "_" + tableName + "Id";

            return GenerateNextId<TEntity>(sequenceName);
        }

        public string GetT_TableName<TEntity>() //Type type, DbContext context)
            where TEntity : class
        {
            var table = GetT_Table_EntitySet<TEntity>(typeof(TEntity), db);
            return (string)table.MetadataProperties["Table"].Value ?? table.Name;
        }

        static EntitySet GetT_Table_EntitySet<TEntity>(Type type, DbContext context)
            where TEntity : class
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

        public long Count<TEntity>()
            where TEntity : class
        {
            return db.Set<TEntity>().AsQueryable<TEntity>().Count();
        }

        public long Count<TEntity>(Expression<Func<TEntity, bool>> predeciate)
            where TEntity : class
        {
            return db.Set<TEntity>().AsQueryable<TEntity>().Count(predeciate);
        }



        public long GenerateNextId<TEntity>(string sequenceName)
            where TEntity : class
        {
            string sql = "SELECT NEXT VALUE FOR dbo." + sequenceName;
            var id = GetObjectContext().ExecuteStoreQuery<long>(sql).First();
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

        public IEnumerable<TEntity> ExecuteStoredProcedure<TEntity>(string query)
            where TEntity : class
        {
            IEnumerable<TEntity> Results = null;
            Results = db.Database.SqlQuery<TEntity>(query).ToList();    
            return Results;
        }

        public IEnumerable<TEntity> ExecuteStoredProcedure<TEntity>(string query, SqlParameter[] parameters)
            where TEntity : class
        {
            IEnumerable<TEntity> Results = null;
            Results = db.Database.SqlQuery<TEntity>(query, parameters).ToList();
            return Results;
        }

        public ObjectContext GetObjectContext()
        {
            return ((IObjectContextAdapter)db).ObjectContext;
        }

        public IQueryable<TEntity> Read<TEntity>(Expression<Func<TEntity, bool>> predicate)
           where TEntity : class
        {
            return db.Set<TEntity>().Where(predicate).AsQueryable();
        }
    }
}
