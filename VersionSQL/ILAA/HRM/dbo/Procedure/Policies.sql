/****** Object:  Procedure [dbo].[Policies]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Policies]
	-- Add the parameters for the stored procedure here
	@LookDesignationId int, 
	@LookPolicyGroupId int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
	if(@LookPolicyGroupId>0)
		 BEGIN
			 select LookPolicy.*,HRPolicy.PolicyValue from LookPolicy 
			 inner join HRPolicy on HRPolicy.LookPolicyId=LookPolicy.LookPolicyId
			 where LookPolicyGroupId=@LookPolicyGroupId and LookDesignationId=@LookDesignationId
		END
		else
		 BEGIN
			 select LookPolicy.*,HRPolicy.PolicyValue from LookPolicy 
			 inner join HRPolicy on HRPolicy.LookPolicyId=LookPolicy.LookPolicyId
			 where LookDesignationId=@LookDesignationId 
		END
	
END
