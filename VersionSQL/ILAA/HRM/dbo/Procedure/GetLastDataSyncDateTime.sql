/****** Object:  Procedure [dbo].[GetLastDataSyncDateTime]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,Wasim Rashid>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetLastDataSyncDateTime]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT top 1 [BiometricSyncId]
      ,[StartDate]
      ,[EndDateTime]
  FROM [HRM].[dbo].[BiometricSync]
  order by [BiometricSyncId] desc

END
