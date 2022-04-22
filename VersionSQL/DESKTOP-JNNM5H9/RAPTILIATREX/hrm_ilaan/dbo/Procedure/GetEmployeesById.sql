/****** Object:  Procedure [dbo].[GetEmployeesById]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,wasim >
-- Create date: <Create Date,,05-01-2022>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetEmployeesById]
	-- Add the parameters for the stored procedure here
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
      FROM [hrm_ilaan].[dbo].[Employee]  emp
	   inner join [hrm_ilaan].[dbo].[Employee] mang on mang.EmployeeId=emp.ManagerId
	    where emp.EmployeeId=@EmployeeId
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
      FROM  [hrm_ilaan].[dbo].[Employee]  a inner join [hrm_ilaan].[dbo].[Employee] c on c.EmployeeId=a.ManagerId INNER JOIN RecQry b
        ON a.[ManagerId] = b.EmployeeId
)
SELECT *
  FROM RecQry



END
