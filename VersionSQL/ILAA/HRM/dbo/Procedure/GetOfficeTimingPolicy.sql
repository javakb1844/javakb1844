/****** Object:  Procedure [dbo].[GetOfficeTimingPolicy]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,wasim>
-- Create date: <Create Date,,05-01-2022>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetOfficeTimingPolicy]
	-- Add the parameters for the stored procedure here
	@HRPolicyCaptionId int =2
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
		 SELECT 
      [HRPolicyByCaptionId]
      ,[HRPolicyCaptionId]   
      ,[LookPolicyId]
      ,[PolicyValue]
      ,[GroupTypeId]   
      ,[PolicyName]
      ,[LookPolicyGroupId]	
  FROM [dbo].[HRPolicyByCaption]  

  where HRPolicyCaptionId=@HRPolicyCaptionId
END
