/****** Object:  Procedure [dbo].[addMenuId]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[addMenuId]
	-- Add the parameters for the stored procedure here
@PermissionId int, 
@RoleId int,
@IsRemove int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	if(@IsRemove=0)
	BEGIN
	DECLARE @leftMenuId int
	DECLARE @parentleftMenuId int
	DECLARE @ok nvarchar(max)
	 select @leftMenuId=LeftMenu.LeftMenuId,@parentleftMenuId=LeftMenu.ParentId from LeftMenu inner join Permission on Permission.MenuId=LeftMenu.LeftMenuId where PermissionId=5
	 DECLARE @countMenuId int
     select @countMenuId=count(*) from RoleMenus where RoleId=2 and MenuId=@leftMenuId
		if(@countMenuId=0)
			BEGIN
			insert RoleMenus (MenuId, RoleId) values (@leftMenuId,@RoleId)
			END
		if(@parentleftMenuId>0)
		BEGIN
		DECLARE @countParentMenuId int
		select @countParentMenuId=count(*) from RoleMenus where RoleId=2 and MenuId=@parentleftMenuId
			if(@countParentMenuId=0)
			BEGIN
			insert RoleMenus (MenuId, RoleId) values (@parentleftMenuId,@RoleId)
			END
		END
		
	--select @leftMenuId ,@parentleftMenuId
	END
	
    -- Insert statements for procedure here
	
END
