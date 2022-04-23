/****** Object:  Table [dbo].[RolePermissions]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[RolePermissions](
	[RolePermissionId] [bigint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[RoleId] [bigint] NOT NULL,
	[PermissionId] [bigint] NOT NULL,
	[ProductSaleProfileId] [int] NULL,
	[CreateDate] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
 CONSTRAINT [PK_RolePermissions] PRIMARY KEY CLUSTERED 
(
	[RolePermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[RolePermissions] ADD  CONSTRAINT [DF_RolePermissions_ProductSaleProfileId]  DEFAULT ((0)) FOR [ProductSaleProfileId]
ALTER TABLE [dbo].[RolePermissions] ADD  CONSTRAINT [DF_RolePermissions_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
