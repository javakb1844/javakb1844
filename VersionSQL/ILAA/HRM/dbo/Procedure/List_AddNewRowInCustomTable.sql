/****** Object:  Procedure [dbo].[List_AddNewRowInCustomTable]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[List_AddNewRowInCustomTable]
	-- Add the parameters for the stored procedure here
	@ProductSaleProfileId  int,
	@CName  varchar(500),
	@Cvalue  varchar(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @sqlStr  varchar(5000)
	BEGIN TRY
    -- Insert statements for procedure here
		-- set @sqlStr='INSERT INTO [CUST].['+cast(@ProductSaleProfileId as varchar(20))+'_'+@CName+'] (['++@CName+']) VALUES   ('+@Cvalue+')';
		 set @sqlStr='INSERT INTO [CUST].['+cast(@ProductSaleProfileId as varchar(20))+'_'+@CName+'] (['++@CName+'])  select '''+@Cvalue+''' as '+@CName+' from (
					SELECT count(*) as LeadSourceId
					  FROM [CUST].['+cast(@ProductSaleProfileId as varchar(20))+'_'+@CName+'] 
					  where '+@CName+'='''+@Cvalue+''') abc
					  where abc.LeadSourceId=0';


  print (@sqlStr)
		 exec(@sqlStr)	
		 set  @sqlStr='select TOP (100) * from [CUST].['+cast(@ProductSaleProfileId as varchar(20))+'_'+@CName+']  FOR JSON AUTO';
	      print (@sqlStr)
		exec(@sqlStr)	
	 END TRY
	 BEGIN CATCH
		  SELECT
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_STATE() AS ErrorState,
			ERROR_SEVERITY() AS ErrorSeverity,
			ERROR_PROCEDURE() AS ErrorProcedure,
			ERROR_LINE() AS ErrorLine,
			ERROR_MESSAGE() AS ErrorMessage
			--FOR JSON AUTO;
      END CATCH;
END
