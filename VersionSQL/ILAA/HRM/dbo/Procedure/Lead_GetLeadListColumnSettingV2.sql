/****** Object:  Procedure [dbo].[Lead_GetLeadListColumnSettingV2]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Lead_GetLeadListColumnSettingV2
	-- Add the parameters for the stored procedure here
	@Employeeid bigint,
	@ProductSaleProfileId int,
	@TableId int

AS
BEGIN
declare @vari varchar(max)
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	--set @vari='SELECT  [CustTableInfoLID] 
	--,ct.ColumnId
 --     ,isnull([ParentName],[ASCaption])   as ColumnName   
 --      ,cast(case when [IsVisible]=''v'' then 1 else 0 end as bit)  as  IsVisible  
 -- FROM [CUST].['+cast (@ProductSaleProfileId as varchar(10))+'_CustTableInfo]
 -- where [ParentLID]   =0 and employeeid='+cast (@Employeeid as varchar(10))+' and TableId='+cast (@TableId as varchar(10))+'
 -- and [ProductSaleProfileId]='+cast (@ProductSaleProfileId as varchar(10));

  set @vari='  SELECT  ct.[CustTableInfoLID]  ,ct.ColumnId ,ct.[ParentLID]
      ,isnull(ct.[ParentName],ct.[ASCaption])   as ColumnName   
      , cast(case when  ct.IsVisible=''v'' then 1 else 0 end as bit)  as IsVisible , ctEm.[CustTableInfoLID]  as EmpCustTableInfoLID,cast(case when ctEm.IsVisible=''v'' then 1 else 0 end as bit) as  EmpIsVisible
  FROM [CUST].['+cast (@ProductSaleProfileId as varchar(10))+'_CustTableInfo]  ct
  left join [CUST].['+cast (@ProductSaleProfileId as varchar(10))+'_CustTableInfo]  ctEm  on ctem.columnid=ct.columnid  and ctem.employeeid='+cast (@Employeeid as varchar(10))+'
  where  ct.employeeid=0 and ct.TableId='+cast (@TableId as varchar(10))+'
  and ct.[ProductSaleProfileId]='+cast (@ProductSaleProfileId as varchar(10))

  exec(@vari)
END
