/****** Object:  Procedure [dbo].[GetPolicyByHRPolicyCaptionId]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE GetPolicyByHRPolicyCaptionId
	-- Add the parameters for the stored procedure here
	   @HRPolicyCaptionId int,
	   @ProductSaleProfileId int,
	   @OrganizationId bigint,
	   @LookPolicyGroupId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
       LP.[LookPolicyId]
      , LP.[PolicyName]
      , LP.[PolicyUnit]
      , LP.[LookPolicyGroupId]
      , LP.[ValueType]
      , LP.[IsPercentage]
	  ,HPBC.PolicyValue
	  ,HPBC.TimeIn
	  ,HPBC.TimeOut
	  ,HPBC.HRPolicyByCaptionId
  FROM [dbo].[LookPolicy] LP with(nolock)
  left join  [dbo].[HRPolicyByCaption] HPBC with(nolock) on HPBC.LookPolicyId=LP.LookPolicyId and HPBC.LookPolicyGroupId=LP.LookPolicyGroupId  and HPBC.HRPolicyCaptionId=@HRPolicyCaptionId and HPBC.LookOrganizationId=@OrganizationId
  and HPBC.ProductSaleProfileId=@ProductSaleProfileId
  where LP.LookPolicyGroupId=@LookPolicyGroupId

END
