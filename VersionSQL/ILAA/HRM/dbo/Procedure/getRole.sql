/****** Object:  Procedure [dbo].[getRole]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getRole]
	-- Add the parameters for the stored procedure here
	   @RoleId int ,
	   @ProductSaleProfileId int,
	   @OrganizationId bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	if(@RoleId=0)
	BEGIN
				select  LeftMenu.* 
			from LeftMenu inner join Permission on LeftMenu.LeftMenuId = Permission.MenuId 
			
									where LeftMenu.IsActive = 1 
								  --  order by LeftMenu.DisplayOrder

			union
									select * from  LeftMenu where LeftMenu.IsActive = 1  and LeftMenuId in (
									select distinct lf.NewParentId as MenuId
									from
									Permission p
									inner join LeftMenu lf on lf.LeftMenuId=p.MenuId)
									
			union

			select * from  LeftMenu where LeftMenu.IsActive = 1  and LeftMenuId in 
									(select NewParentId from  LeftMenu where LeftMenuId in (
									select distinct lf.NewParentId as MenuId
									from
									Permission p
									inner join LeftMenu lf on lf.LeftMenuId=p.MenuId))
	end
						else
	BEGIN
	select  LeftMenu.* 
			from LeftMenu inner join Permission on LeftMenu.LeftMenuId = Permission.MenuId    
			  inner join RolePermissions on RolePermissions.PermissionId = Permission.PermissionId						
                        where LeftMenu.IsActive = 1 and RolePermissions.RoleId = @RoleId and RolePermissions.ProductSaleProfileId=@ProductSaleProfileId
							

			union
									select * from  LeftMenu where LeftMenu.IsActive = 1  and LeftMenuId in (
									select distinct lf.NewParentId as MenuId
									from
									Permission p
									inner join LeftMenu lf on lf.LeftMenuId=p.MenuId
									 inner join RolePermissions rp on rp.PermissionId = p.PermissionId	
									where lf.IsActive = 1 and rp.RoleId = @RoleId and rp.ProductSaleProfileId=@ProductSaleProfileId									
									)
									 and LeftMenu.IsActive = 1
			union

			select * from  LeftMenu where LeftMenu.IsActive = 1  and LeftMenuId in 
									(select NewParentId from  LeftMenu where LeftMenuId in (
									select distinct lf.NewParentId as MenuId
									from
									Permission p
									inner join LeftMenu lf on lf.LeftMenuId=p.MenuId
									 inner join RolePermissions rp on rp.PermissionId = p.PermissionId	
									 where lf.IsActive = 1 and rp.RoleId = @RoleId and rp.ProductSaleProfileId=@ProductSaleProfileId	
									) and LeftMenu.isactive=1 )  and LeftMenu.isactive=1

	end
END
