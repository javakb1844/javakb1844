/****** Object:  Procedure [dbo].[GetEmployees]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,wasim >
-- Create date: <Create Date,,05-01-2022>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetEmployees]
	-- Add the parameters for the stored procedure here
	@LookOrganizationId bigint ,
	@LookOrganizationIds  varchar(200)=''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @query varchar(5000)
    -- Insert statements for procedure here
	if(@LookOrganizationIds!='')
	begin
	set @query='SELECT TOP (1000) emp.[EmployeeId]
      ,emp.[FullName]
      ,emp.[EmployeeGuid]
      ,emp.[EmployeeNum]     
      ,emp.[Email]    
      ,emp.[Gender]     
      ,emp.[CNIC]
      ,emp.[IsDisable]   
      ,emp.[MobileNo] 
      ,emp.[DateOfJoining]
      ,emp.[ManagerId]     
      ,emp.[BioMetricId]
	  ,ld.DepartmentName
	  ,mang.FullName as ManagerName
  FROM
 [dbo].[Employee] emp
 left join  [dbo].LookDepartment ld on ld.LookDepartmentId=emp.LookDepartmentId
  left join [dbo].[Employee] mang on mang.EmployeeId=emp.ManagerId
  where emp.LookOrganizationId='+cast( @LookOrganizationId as varchar(10))+' and  (emp.ExitDate is null or emp.ExitDate <  DATEADD(month, -2,  getdate()) )  and (emp.IsUserOnly=0  or emp.IsUserOnly is null)' ; -- in ('+@LookOrganizationIds+')';
  end
  else 
  begin
  set @query='SELECT TOP (1000) emp.[EmployeeId]
      ,emp.[FullName]
      ,emp.[EmployeeGuid]
      ,emp.[EmployeeNum]     
      ,emp.[Email]    
      ,emp.[Gender]     
      ,emp.[CNIC]
      ,emp.[IsDisable]   
      ,emp.[MobileNo] 
      ,emp.[DateOfJoining]
      ,emp.[ManagerId]     
      ,emp.[BioMetricId]
	   ,ld.DepartmentName
	  ,mang.FullName as ManagerName
  FROM
 [dbo].[Employee] emp
  left join  [dbo].LookDepartment ld on ld.LookDepartmentId=emp.LookDepartmentId
  left join [dbo].[Employee] mang on mang.EmployeeId=emp.ManagerId
  where emp.LookOrganizationId='+cast( @LookOrganizationId as varchar(10))+' and  (emp.ExitDate is null or emp.ExitDate <  DATEADD(month, -2,  getdate()) )   and (emp.IsUserOnly=0  or emp.IsUserOnly is null)';
  end
   exec(@query)	
END
