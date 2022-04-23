/****** Object:  Procedure [dbo].[CRMAddColumnInTable]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE CRMAddColumnInTable
	-- Add the parameters for the stored procedure here
	@TableName varchar(200),
	@NewColumnName varchar(200),
	@NewColumnDataType varchar (50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @vari varchar(3000)
    -- Insert statements for procedure here
	set @vari='alter table  [cust].['+@TableName+'] add  '+@NewColumnName+' '+  @NewColumnDataType + ' DEFAULT ((0)) ';
	print (@vari)
	 exec(@vari)	
 
END
