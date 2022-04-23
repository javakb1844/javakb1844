/****** Object:  Procedure [dbo].[GetLeadByResponsiblePersonv2]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE GetLeadByResponsiblePersonv2 
	-- Add the parameters for the stored procedure here
	@ProductSaleProfileId int,
	@EmployeeId bigint,
	@SelectQuery varchar(max)=' [lm].[LID] as [LID/LID], 1 as [LID/Visible]
	  ,[lm].[LeadName] as [LeadName/LeadName], 1 as [LeadName/Visible]
      ,[lm].[LeadNumber] as [LeadNumber/LeadNumber], 1 as [LeadNumber/visible]
      ,[lm].[LeadSourceId] as [LeadSource/Id], 1 as [LeadSource/Visible],  (SELECT   LeadSource  FROM cust.[4_LeadSource] where LeadSourceId=[lm].[LeadSourceId]  ) as [LeadSource/Name],   (SELECT   [LeadSourceId],LeadSource   FROM cust.[4_LeadSource]   FOR JSON AUTO) as [LeadSource/Data]     
      ,[lm].[LeadStatusId] as [LeadStatus/Id], (SELECT   LeadStatus  FROM cust.[4_LeadStatus] where LeadStatusId=[lm].[LeadStatusId]  ) as [LeadStatus/Name],   (SELECT   [LeadStatusId],LeadStatus   FROM cust.[4_LeadStatus]   FOR JSON AUTO) as [LeadStatus/Data]  
      ,[lm].[LeadEmail] as [LeadEmail/LeadEmail], 1 as [LeadEmail/Visible]
      ,[lm].[CreatedDate]  as [CreatedDate/CreatedDate], 1 as [CreatedDate/Visible]
      ,[lm].[CreatedDateUTC] as [CreatedDateUTC/CreatedDateUTC], 1 as [CreatedDateUTC/Visible]
      ,[lm].[LastModifiedDate] as [LastModifiedDate/LastModifiedDate], 1 as [LastModifiedDate/Visible]
      ,[lm].[LastModifiedDateUTC] as [LastModifiedDateUTC/LastModifiedDateUTC], 1 as [LastModifiedDateUTC/Visible]
      ,[lm].[CreatedByLID] as [CreatedBy/LID],(SELECT   fullname  FROM dbo.employee where employeeid=[lm].[CreatedByLID]  ) as [CreatedBy/Name]  
      ,[lm].[LastModifiedByLID] as [LastModifiedByLID/LID], 1 as [LastModifiedByLID/Visible]   ,(SELECT   fullname  FROM dbo.employee where employeeid=[lm].[LastModifiedByLID]  ) as [LastModifiedByLID/Name]   
      ,[lm].[AsignToLID] as [AsignToLId/LID], 1 as [AsignToLId/Visible],    (SELECT   fullname  FROM dbo.employee where employeeid=[lm].[AsignToLID]  ) as [AsignToLId/Name]    '
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @vari varchar(max)
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
