/****** Object:  Procedure [dbo].[Lead_GetLeadListSlectionColumn]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Lead_GetLeadListSlectionColumn]
	-- Add the parameters for the stored procedure here
    @ProductSaleProfileId int,
	@EmployeeId bigint,
	@TableId  int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	if(@TableId=1)
	begin
	SELECT     
    ct.Employeeid,ct.[ColumnName]+' as ['+ct.IsVisible+'_' +isnull( ct.[ParentName],'')+ct.ASCaption+']' as ASCaption    
  FROM [CUST].[4_CustTableInfo] ct
  where ct.Employeeid in (0,@EmployeeId)
 and ct.[ProductSaleProfileId]=@ProductSaleProfileId and   ct.[TableId]=@TableId and ct.IsVisible='v'
	 order by  ct.[ColumnId] --ct.Employeeid,ParentName 
    -- Insert statements for procedure here
	end
	else 
	begin
		SELECT     
    ct.Employeeid,ct.[ColumnName]+' as ['+ct.IsVisible+cast(ct.IsReadOnly as varchar(3))+cast(ct.IsCompulsory as varchar(3))+'_' +isnull( ct.[ParentName],'')+ct.ASCaption+']' as ASCaption    
  FROM [CUST].[4_CustTableInfo] ct
  where ct.Employeeid in (0,@EmployeeId)
 and ct.[ProductSaleProfileId]=@ProductSaleProfileId and   ct.[TableId]=@TableId and ct.IsVisible='v'
	 order by  ct.[ColumnId]
	end
	
END
