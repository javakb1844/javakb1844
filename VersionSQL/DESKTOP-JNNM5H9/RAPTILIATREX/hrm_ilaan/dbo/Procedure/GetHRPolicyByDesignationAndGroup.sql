/****** Object:  Procedure [dbo].[GetHRPolicyByDesignationAndGroup]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,wasim>
-- Create date: <Create Date,,05-01-2022>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE GetHRPolicyByDesignationAndGroup
	-- Add the parameters for the stored procedure here
	@LookDesignationId int =0,
	@GroupTypeId int =1
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
       hrp.[HRPolicyId]
      ,hrp.[LookDesignationId]
      ,hrp.[LookPolicyId]
      ,hrp.[PolicyValue]    
	  ,lp.PolicyName
	  ,lp.PolicyUnit
	  ,lp.[LookPolicyGroupId]
	 -- ,lpg.PolicyGroupName
  FROM [hrm_ilaan].[dbo].[HRPolicy] hrp 
  inner join [hrm_ilaan].[dbo].[LookPolicy]  lp on lp.LookPolicyId=hrp.LookPolicyId
  -- inner join [hrm_ilaan].[dbo].LookPolicyGroup  lpg on lpg.[LookPolicyGroupId]=hrp.[GroupTypeId]
  where hrp.[GroupTypeId]=1 and hrp.[LookDesignationId]=@LookDesignationId
END
