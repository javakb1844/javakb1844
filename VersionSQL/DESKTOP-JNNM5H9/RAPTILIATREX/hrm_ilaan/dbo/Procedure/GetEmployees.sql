/****** Object:  Procedure [dbo].[GetEmployees]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,wasim >
-- Create date: <Create Date,,05-01-2022>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetEmployees]
	-- Add the parameters for the stored procedure here
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
	  ,mang.FullName as ManagerName
  FROM
  [hrm_ilaan].[dbo].[Employee] emp
  left join [hrm_ilaan].[dbo].[Employee] mang on mang.EmployeeId=emp.ManagerId
  where emp.ExitDate is null and emp.LookOrganizationId in ('+@LookOrganizationIds+')';
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
	  ,mang.FullName as ManagerName
  FROM
  [hrm_ilaan].[dbo].[Employee] emp
  left join [hrm_ilaan].[dbo].[Employee] mang on mang.EmployeeId=emp.ManagerId
  where emp.ExitDate is null';
  end
   exec(@query)	
END
