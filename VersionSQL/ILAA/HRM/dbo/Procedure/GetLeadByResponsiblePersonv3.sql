/****** Object:  Procedure [dbo].[GetLeadByResponsiblePersonv3]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetLeadByResponsiblePersonv3]
	-- Add the parameters for the stored procedure here
	@ProductSaleProfileId int,
	@EmployeeId bigint,	
	@leadid bigint,
 @SelectQuery varchar(max)='[lm].[LID] as [v_LID]
,[lm].[LeadName] as [v_LeadName]
,[lm].[LeadNumber] as [v_LeadNumber]
,[lm].[LeadEmail] as [a_LeadEmail]
,[lm].[CreatedDate] as [a_CreatedDate]
,[lm].[CreatedDateUTC] as [v_CreatedDateUTC]
,[lm].[LastModifiedDate] as [a_LastModifiedDate]
,[lm].[LastModifiedDateUTC] as [v_LastModifiedDateUTC]
,[lm].[AsignToLID] as [v_AsignToLID/LID]
, (SELECT   fullname  FROM dbo.employee where employeeid=[lm].[AsignToLID]  )  as [v_AsignToLID/Name]
,[lm].[CreatedByLID] as [v_CreatedByLID/LID]
,(SELECT   fullname  FROM dbo.employee where employeeid=[lm].[CreatedByLID]  )  as [v_CreatedByLID/Name]
,[lm].[LastModifiedByLID] as [v_LastModifiedByLID/LID]
,(SELECT   fullname  FROM dbo.employee where employeeid=[lm].[LastModifiedByLID]  )  as [v_LastModifiedByLID/Name]
,[lm].[LeadSourceId] as [v_LeadSourceId/Id]
,(SELECT   [LeadSourceId],LeadSource   FROM cust.[4_LeadSource]   FOR JSON AUTO)  as [v_LeadSourceId/Data]
,(SELECT   LeadSource  FROM cust.[4_LeadSource] where LeadSourceId=[lm].[LeadSourceId]  ) as [v_LeadSourceId/Name]
,[lm].[LeadStatusId] as [v_LeadStatusId/Id]
,(SELECT   LeadStatus  FROM cust.[4_LeadStatus] where LeadStatusId=[lm].[LeadStatusId]  ) as [v_LeadStatusId/Name]
,(SELECT   [LeadStatusId],LeadStatus   FROM cust.[4_LeadStatus]   FOR JSON AUTO)  as [v_LeadStatusId/Data]'
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @vari varchar(max)
	declare @Where varchar(2000)=''
	if(@leadid>0)
	begin
	set @Where=' and [lm].[LID]='+cast (@leadid as varchar(10))
	end
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
inner join (SELECT  EmployeeId FROM RecQry)  rc on lm.asignTolId=rc.EmployeeId where 1=1 '+ @Where+'  FOR XML PATH(''Lead''), ROOT(''Leads'')';
print  (@vari)
  exec(@vari)
END
