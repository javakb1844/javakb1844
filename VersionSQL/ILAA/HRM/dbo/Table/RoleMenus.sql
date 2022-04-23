/****** Object:  Table [dbo].[RoleMenus]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[RoleMenus](
	[RoleMenuId] [bigint] IDENTITY(1,1) NOT NULL,
	[MenuId] [bigint] NOT NULL,
	[RoleId] [bigint] NOT NULL,
	[DefaultControllerName] [nvarchar](100) NULL,
	[DefaultActionName] [nvarchar](100) NULL,
	[IsDefault] [bit] NULL,
	[ProductSaleProfileId] [int] NULL,
 CONSTRAINT [PK_RoleMenu] PRIMARY KEY CLUSTERED 
(
	[RoleMenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[RoleMenus] ADD  CONSTRAINT [DF_RoleMenus_isDefault]  DEFAULT ((0)) FOR [IsDefault]
ALTER TABLE [dbo].[RoleMenus] ADD  CONSTRAINT [DF_RoleMenus_ProductSaleProfileId]  DEFAULT ((0)) FOR [ProductSaleProfileId]
