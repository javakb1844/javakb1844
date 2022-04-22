/****** Object:  Procedure [dbo].[deleteLeave]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[deleteLeave]
	-- Add the parameters for the stored procedure here
	@EmployeeId int, 
	@EmployeeLeaveSummaryId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	

	delete from  EmployeeLeaveSummary  where  EmployeeId=@EmployeeId and LookLeaveStatusId<2 and EmployeeLeaveSummaryId=@EmployeeLeaveSummaryId;
    delete from EmployeeLeave  where EmployeeId=@EmployeeId and LookLeaveStatusId<2 and EmployeeLeaveSummaryId=@EmployeeLeaveSummaryId;

    -- Insert statements for procedure here
	
END
