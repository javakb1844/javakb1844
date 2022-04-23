/****** Object:  Procedure [dbo].[GetAttandenceByDateAndBioId]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,Wasim 05-01-2022>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAttandenceByDateAndBioId]
	-- Add the parameters for the stored procedure here
	 @AttDate date,	
	 @biometricid int,
	 @ProductSaleProfileId int,
	 @OrganizationId bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		SELECT 
	 b.[BioMetricId]
   ,b.[DateTimeRecord] 
	  ,b.[DateOnly]
	  ,e.FullName
	  ,b.[dwInOutMode]
	  ,b.IsEdit
	  FROM [dbo].[BioMetricData] b
	  inner join [dbo].[Employee] e on e.BioMetricId=b.BioMetricId and b.ProductSaleProfileId=e.ProductSaleProfileId
	   where 1=1 
	   --and  [dwInOutMode]=0 
	   and b.ProductSaleProfileId=@ProductSaleProfileId
	   and (e.IsUserOnly=0  or e.IsUserOnly is null)
	   and b.[DateOnly]=@AttDate
	   and  b.[BioMetricId]=@biometricid
END
