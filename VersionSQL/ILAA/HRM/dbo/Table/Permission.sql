/****** Object:  Table [dbo].[Permission]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[Permission](
	[PermissionId] [bigint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Attribute] [nvarchar](100) NULL,
	[Name] [nvarchar](100) NULL,
	[MenuId] [bigint] NULL,
	[LookDepartmentId] [bigint] NULL,
	[PermissionLevel] [int] NOT NULL,
 CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
(
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY],
 CONSTRAINT [UQ__Permissi__2BDE4DF6761848B8] UNIQUE NONCLUSTERED 
(
	[Attribute] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__Permissi__2BDE4DF6D823907C] UNIQUE NONCLUSTERED 
(
	[Attribute] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[Permission] ADD  CONSTRAINT [DF_Permission_IsActive]  DEFAULT ((0)) FOR [IsActive]
ALTER TABLE [dbo].[Permission] ADD  CONSTRAINT [DF_Permission_IsDeveloperOnly]  DEFAULT ((0)) FOR [PermissionLevel]
