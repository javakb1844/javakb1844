/****** Object:  Procedure [dbo].[Lead_AddNewLeadListColumnSettingForUser]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Lead_AddNewLeadListColumnSettingForUser]
	-- Add the parameters for the stored procedure here
	      	@EmployeeId bigint,
		  @ProductSaleProfileId int,
		  @TableId int,
		  @ColumnId varchar(3000)
		
          
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @vari varchar(max)
    -- Insert statements for procedure here
	set @vari='INSERT INTO [CUST].['+cast (@ProductSaleProfileId as varchar(10))+'_CustTableInfo]
           ([TableId]
           ,[ColumnName]
           ,[ParentName]
           ,[ASCaption]
           ,[IsVisible]
           ,[DataSelect]
           ,[ProductSaleProfileId]
           ,[LookColumnTypeId]
           ,[TableName]
           ,[ParentLID]
           ,[QueryType]
           ,[EmployeeId]
           ,[ColumnId])
    SELECT 
      [TableId]
      ,[ColumnName]
      ,[ParentName]
      ,[ASCaption]
      ,''a'' as [IsVisible]
      ,[DataSelect]
      ,[ProductSaleProfileId]
      ,[LookColumnTypeId]
      ,[TableName]
      ,[ParentLID]
      ,[QueryType]
      ,'+cast (@EmployeeId as varchar(10))+' as [EmployeeId]
      ,[ColumnId]
  FROM [CUST].['+cast (@ProductSaleProfileId as varchar(10))+'_CustTableInfo]
  where  [EmployeeId]=0 and   [TableId]='+cast (@TableId as varchar(10))+' and [ProductSaleProfileId]='+cast (@ProductSaleProfileId as varchar(10))+' order by [ColumnId]';
   exec(@vari)
   if(len(@ColumnId)>0)
   begin
  set @vari='update  [CUST].['+cast (@ProductSaleProfileId as varchar(10))+'_CustTableInfo] set [IsVisible]=''v''   where   [EmployeeId]='+cast (@EmployeeId as varchar(10))+'  and [ProductSaleProfileId]='+cast (@ProductSaleProfileId as varchar(10))+' and   [TableId]='+cast (@TableId as varchar(10))+' and ColumnId in ('+ @ColumnId+')'
  exec(@vari)
 end
END
