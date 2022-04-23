/****** Object:  Procedure [dbo].[Lead_UpdateNewLeadListColumnSettingForUser]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Lead_UpdateNewLeadListColumnSettingForUser]
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
	--set @vari='update  [CUST].['+cast (@ProductSaleProfileId as varchar(10))+'_CustTableInfo] set [IsVisible]=''a''   where  CustTableInfoLID in ( select CustTableInfoLID from
 -- [CUST].['+cast (@ProductSaleProfileId as varchar(10))+'_CustTableInfo]
 -- where   [EmployeeId]='+cast (@EmployeeId as varchar(10))+'  and [ProductSaleProfileId]='+cast (@ProductSaleProfileId as varchar(10))+' and   [TableId]='+cast (@TableId as varchar(10))+')'; 
 set @vari='update  [CUST].['+cast (@ProductSaleProfileId as varchar(10))+'_CustTableInfo] set [IsVisible]=''a''  
  where   [EmployeeId]='+cast (@EmployeeId as varchar(10))+'  and [ProductSaleProfileId]='+cast (@ProductSaleProfileId as varchar(10))+' and   [TableId]='+cast (@TableId as varchar(10)); 

 exec(@vari)
 print ( @vari)
   if(len(@ColumnId)>0)
   begin  
  -- set @vari='update  [CUST].['+cast (@ProductSaleProfileId as varchar(10))+'_CustTableInfo] set [IsVisible]=''v''   where  CustTableInfoLID in ( select CustTableInfoLID from [CUST].['+cast (@ProductSaleProfileId as varchar(10))+'_CustTableInfo] where [EmployeeId]='+cast (@EmployeeId as varchar(10))+'  and [ProductSaleProfileId]='+cast (@ProductSaleProfileId as varchar(10))+' and   [TableId]='+cast (@TableId as varchar(10))+' and  ([ColumnId] in ('+ @ColumnId+') or ParentLID in ('+ @ColumnId+') ))';
      set @vari='update  [CUST].['+cast (@ProductSaleProfileId as varchar(10))+'_CustTableInfo] set [IsVisible]=''v''    where [EmployeeId]='+cast (@EmployeeId as varchar(10))+'  and [ProductSaleProfileId]='+cast (@ProductSaleProfileId as varchar(10))+' and   [TableId]='+cast (@TableId as varchar(10))+' and  ([ColumnId] in ('+ @ColumnId+') or ParentLID in ('+ @ColumnId+') )';

 exec(@vari)
 print ( @vari)
 end
END
