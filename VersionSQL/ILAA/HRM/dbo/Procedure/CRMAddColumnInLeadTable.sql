/****** Object:  Procedure [dbo].[CRMAddColumnInLeadTable]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CRMAddColumnInLeadTable]
	-- Add the parameters for the stored procedure here
	@TableName varchar(200),
	@NewColumnName varchar(200),
	
	@ColumnTrId int,
	@ProductSaleProfileId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare  @vari varchar(3000)
	declare @NewColumnDataType varchar (50)
    -- Insert statements for procedure here
	if(@ColumnTrId=6)
	begin
			set @NewColumnDataType=' varchar(1000) ';
			set @vari='ALTER TABLE  '+@TableName+' add  '+@NewColumnName+' '+  @NewColumnDataType + 'NULL ';
			--set @vari='alter table  [cust].['+@TableName+'] add  '+@NewColumnName+' '+  @NewColumnDataType + ' DEFAULT ((0)) ';
			print (@vari)
			-- exec(@vari)
			set @vari='INSERT INTO [CUST].['+cast (@ProductSaleProfileId as varchar(13))+'_TableColumnInfo]
						   ([ColumnName]                          ,[ColumnNameDB],  [ParentName],          [ASCaption]                  ,[TableName]    ,[ColumnCatId] ,[ColumnType]          ,[CreateDate] ,[CreatedBy] ,[ColumnStatudId]  ,[UpdatedDate]  ,[UpdatedBy] ,[ParentLId] ,[ProductSaleProfileId],[ForginKeyTableName])
				VALUES     ('''+'[lm].['+@NewColumnName+']'' ,  '''+@NewColumnName+''',Null ,         '''+@NewColumnName+'''      ,'''+@TableName+'''  ,1   ,    '''+@NewColumnDataType+'''  ,getdate()     ,0            ,1                     ,NULL         ,0  ,        0         ,'+cast (@ProductSaleProfileId as varchar(13))+'  ,NULL)';
			print (@vari)
--exec(@vari)
	 end
	else if(@ColumnTrId=7)
	begin
			set @NewColumnDataType=' int ';
			set @vari='ALTER TABLE  '+@TableName+' add  '+@NewColumnName+' '+  @NewColumnDataType + '  DEFAULT ((0)) ';
		--	set @vari='alter table  [cust].['+@TableName+'] add  '+@NewColumnName+' '+  @NewColumnDataType + ' DEFAULT ((0)) ';
			print (@vari)
			-- exec(@vari)
			set @vari='INSERT INTO [CUST].['+cast (@ProductSaleProfileId as varchar(13))+'_TableColumnInfo]
						   ([ColumnName]                          ,[ColumnNameDB],  [ParentName],          [ASCaption]                  ,[TableName]    ,[ColumnCatId] ,[ColumnType]          ,[CreateDate] ,[CreatedBy] ,[ColumnStatudId]  ,[UpdatedDate]  ,[UpdatedBy] ,[ParentLId] ,[ProductSaleProfileId],[ForginKeyTableName])
				VALUES     ('''+'[lm].['+@NewColumnName+']'' ,  '''+@NewColumnName+''',Null ,         '''+@NewColumnName+'''      ,'''+@TableName+'''  ,1   ,    '''+@NewColumnDataType+'''  ,getdate()     ,0            ,1                     ,NULL         ,0  ,        0         ,'+cast (@ProductSaleProfileId as varchar(13))+'  ,NULL)';
			print (@vari)
--exec(@vari)
	 end
 else if(@ColumnTrId=8)
	begin
			set @NewColumnDataType=' DateTime ';
			set @vari='ALTER TABLE  '+@TableName+' add  '+@NewColumnName+' '+  @NewColumnDataType + '  NULL ';
		--	set @vari='alter table  [cust].['+@TableName+'] add  '+@NewColumnName+' '+  @NewColumnDataType + ' DEFAULT ((0)) ';
			print (@vari)
			-- exec(@vari)
			set @vari='INSERT INTO [CUST].['+cast (@ProductSaleProfileId as varchar(13))+'_TableColumnInfo]
						   ([ColumnName]                          ,[ColumnNameDB],  [ParentName],          [ASCaption]                  ,[TableName]    ,[ColumnCatId] ,[ColumnType]          ,[CreateDate] ,[CreatedBy] ,[ColumnStatudId]  ,[UpdatedDate]  ,[UpdatedBy] ,[ParentLId] ,[ProductSaleProfileId],[ForginKeyTableName])
				VALUES     ('''+'[lm].['+@NewColumnName+']'' ,  '''+@NewColumnName+''',Null ,         '''+@NewColumnName+'''      ,'''+@TableName+'''  ,1   ,    '''+@NewColumnDataType+'''  ,getdate()     ,0            ,1                     ,NULL         ,0  ,        0         ,'+cast (@ProductSaleProfileId as varchar(13))+'  ,NULL)';
			print (@vari)
--exec(@vari)
	 end
END
