/****** Object:  Table [dbo].[LeftMenu]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[LeftMenu](
	[LeftMenuId] [bigint] IDENTITY(1,1) NOT NULL,
	[MenuName] [nvarchar](150) NULL,
	[ControllerName] [nvarchar](150) NULL,
	[ActionName] [nvarchar](150) NULL,
	[IconClass] [nvarchar](150) NULL,
	[IsActive] [bit] NOT NULL,
	[ParentId] [bigint] NOT NULL,
	[HaveChild] [bit] NOT NULL,
	[DisplayOrder] [bigint] NULL,
 CONSTRAINT [PK_LeftMenu] PRIMARY KEY CLUSTERED 
(
	[LeftMenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[LeftMenu] ADD  CONSTRAINT [DF_LeftMenu_IsActive]  DEFAULT ((0)) FOR [IsActive]
ALTER TABLE [dbo].[LeftMenu] ADD  CONSTRAINT [DF_LeftMenu_ParentId]  DEFAULT ((0)) FOR [ParentId]
ALTER TABLE [dbo].[LeftMenu] ADD  CONSTRAINT [DF_LeftMenu_HaveChild]  DEFAULT ((0)) FOR [HaveChild]
ALTER TABLE [dbo].[LeftMenu] ADD  CONSTRAINT [DF_LeftMenu_DisplayOrder]  DEFAULT ((0)) FOR [DisplayOrder]
