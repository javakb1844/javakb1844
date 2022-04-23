/****** Object:  Procedure [dbo].[GetLeadByResponsiblePerson]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE GetLeadByResponsiblePerson 
	-- Add the parameters for the stored procedure here
	@ProductSaleProfileId int,
	@EmployeeId bigint,
	@SelectQuery varchar(5000)=' [lm].[LID],[lm].[LeadName]
      ,[lm].[LeadNumber]
      ,[lm].[LeadSourceId], (SELECT   LeadSource  FROM cust.[4_LeadSource] where LeadSourceId=[lm].[LeadSourceId]  ) as LeadSourceIdName,   (SELECT   [LeadSourceId],LeadSource   FROM cust.[4_LeadSource]   FOR JSON AUTO) as LeadSourceId_Data     
      ,[lm].[LeadStatusId], (SELECT   LeadStatus  FROM cust.[4_LeadStatus] where LeadStatusId=[lm].[LeadStatusId]  ) as LeadStatusIdName,   (SELECT   [LeadStatusId],LeadStatus   FROM cust.[4_LeadStatus]   FOR JSON AUTO) as LeadStatusId_Data  
      ,[lm].[LeadEmail]
      ,[lm].[CreatedDate]
      ,[lm].[CreatedDateUTC]
      ,[lm].[LastModifiedDate]
      ,[lm].[LastModifiedDateUTC]
      ,[lm].[CreatedByLID],(SELECT   fullname  FROM dbo.employee where employeeid=[lm].[CreatedByLID]  ) as CreatedByLIDName  
      ,[lm].[LastModifiedByLID]   ,(SELECT   fullname  FROM dbo.employee where employeeid=[lm].[LastModifiedByLID]  ) as LastModifiedByLIDName   
      ,[lm].[AsignToLID],    (SELECT   fullname  FROM dbo.employee where employeeid=[lm].[AsignToLID]  ) as AsignToLIdName    '
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @vari varchar(2000)
    -- Insert statements for procedure here
 set @vari=' WITH RecQry AS
(
    SELECT emp.[EmployeeId]
      
      FROM [dbo].[Employee]  emp
	   inner join [dbo].[Employee] mang on mang.EmployeeId=isnull(emp.ManagerId,1)  
	    where  emp.ProductSaleProfileId ='+cast (@ProductSaleProfileId as varchar(10))+' and emp.EmployeeId='+cast (@EmployeeId as varchar(10))+' and (emp.IsUserOnly=0  or emp.IsUserOnly is null)
    UNION ALL
    SELECT 
	 a.[EmployeeId]
     
      FROM  [dbo].[Employee]  a inner join [dbo].[Employee] c on c.EmployeeId=a.ManagerId INNER JOIN RecQry b
        ON a.[ManagerId] = b.EmployeeId
		where   a.ProductSaleProfileId='+cast (@ProductSaleProfileId as varchar(10))+' and (a.IsUserOnly=0  or a.IsUserOnly is null)
)
select '+@SelectQuery+' from [cust].['+cast (@ProductSaleProfileId as varchar(10))+'_leadmaster] lm
inner join (SELECT  EmployeeId FROM RecQry)  rc on lm.asignTolId=rc.EmployeeId    FOR XML PATH(''Lead''), ROOT(''Leads'')';
print  (@vari)
  exec(@vari)
END
