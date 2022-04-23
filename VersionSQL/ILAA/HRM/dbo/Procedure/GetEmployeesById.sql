/****** Object:  Procedure [dbo].[GetEmployeesById]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,wasim >
-- Create date: <Create Date,,05-01-2022>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetEmployeesById]
	-- Add the parameters for the stored procedure here
	@LookOrganizationId bigint ,
	@EmployeeId bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	  WITH RecQry AS
(
    SELECT emp.[EmployeeId]
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
      FROM [dbo].[Employee]  emp
	   inner join [dbo].[Employee] mang on mang.EmployeeId=isnull(emp.ManagerId,1)  
	    where  emp.LookOrganizationId=@LookOrganizationId and emp.EmployeeId=@EmployeeId and (emp.IsUserOnly=0  or emp.IsUserOnly is null)
    UNION ALL
    SELECT 
	 a.[EmployeeId]
      ,a.[FullName]
      ,a.[EmployeeGuid]
      ,a.[EmployeeNum]     
      ,a.[Email]    
      ,a.[Gender]     
      ,a.[CNIC]
      ,a.[IsDisable]   
      ,a.[MobileNo] 
      ,a.[DateOfJoining]
      ,a.[ManagerId]     
      ,a.[BioMetricId]
	  ,c.FullName as ManagerName
      FROM  [dbo].[Employee]  a inner join [dbo].[Employee] c on c.EmployeeId=a.ManagerId INNER JOIN RecQry b
        ON a.[ManagerId] = b.EmployeeId
		where   a.LookOrganizationId=@LookOrganizationId and (a.IsUserOnly=0  or a.IsUserOnly is null)
)
SELECT *
  FROM RecQry



END
