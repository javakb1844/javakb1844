/****** Object:  Procedure [dbo].[LeaveStatusUpdate]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[LeaveStatusUpdate]
	-- Add the parameters for the stored procedure here
	@LookLeaveStatusId int, 	 
	@EmployeeLeaveSummaryId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	update EmployeeLeaveSummary set LookLeaveStatusId=@LookLeaveStatusId, StatusUpdateDate=GETDATE() where EmployeeLeaveSummaryId=@EmployeeLeaveSummaryId;
    update EmployeeLeave set LookLeaveStatusId=@LookLeaveStatusId where EmployeeLeaveSummaryId=@EmployeeLeaveSummaryId;
	-- Insert statements for procedure here
	
END
