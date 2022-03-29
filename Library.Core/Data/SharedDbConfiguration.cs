using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Data
{
    /* =============================================================================================
     * Single DbConfiguration file shared across all EF projects throughout application(s)
     * 
     * Pre_Requisites:
     *      - Db Schema Requirements below in this document, to support transaction.commit failure auto-handling
     *      
     *      - apply attribute to DbContext class in each project i.e. 
     *          [DbConfigurationType(typeof(Library.Core.Data.SharedDbConfiguration))]
     *          public partial class PUDB
     *          
     *      
     * EF Db Schema to auto-track-transactions, see feature EF & Transaction commit failure handling
     * 
     *      CREATE TABLE [dbo].[__TransactionHistory] (
     *          [Id]           UNIQUEIDENTIFIER NOT NULL,
     *          [CreationTime] DATETIME         NOT NULL,
     *          CONSTRAINT [PK_dbo.__TransactionHistory] PRIMARY KEY CLUSTERED ([Id] ASC)
     *      );     
     * 
     * 
     * Articles:
     *      code based DbConfiguration: http://msdn.microsoft.com/en-us/data/jj680699
     *      
     * Issues:
     *      Issues with ExecutionStrategy & user transactions: http://msdn.microsoft.com/en-US/data/dn307226
     *      Do not work with TransactionScope (user transaction)
     *
     * EDMX & SharedDbConfiguration Issue
     * https://entityframework.codeplex.com/workitem/list/basic?size=2147483647
     * https://entityframework.codeplex.com/workitem/2448
     * 
     * =============================================================================================
     */

    //

    //public class SharedDbConfiguration : DbConfiguration
    //{
    //    public SharedDbConfiguration()
    //    {
    //        Initialize();
    //    }

    //    void Initialize()
    //    {
    //        // 1) Handling of Transaction Commit Failures (EF6.1 Onwards) - auto handled by EF
    //        SetTransactionHandler(SqlProviderServices.ProviderInvariantName, () => new CommitFailureHandler());

    //        // 2) Connection Resiliency refers to the ability for EF to automatically retry any commands that fail due to these connection breaks.
    //        // using default settings
    //        SetExecutionStrategy(SqlProviderServices.ProviderInvariantName, () => new SqlAzureExecutionStrategy());

    //        // using custom settings
    //        //SetExecutionStrategy(SqlProviderServices.ProviderInvariantName, () => new SqlAzureExecutionStrategy(3, TimeSpan.FromSeconds(30)));

    //        // NOTE: do not un-comment it
    //        //SuspendExecutionStrategy = true;
    //    }


    //    //// http://msdn.microsoft.com/en-us/data/dn307226
    //    //public static bool SuspendExecutionStrategy
    //    //{
    //    //    get
    //    //    {
    //    //        return (bool?)CallContext.LogicalGetData("SuspendExecutionStrategy") ?? false;
    //    //    }
    //    //    set
    //    //    {
    //    //        CallContext.LogicalSetData("SuspendExecutionStrategy", value);
    //    //    }
    //    //} 


    //}
}
