/****** Object:  Table [dbo].[EmployeeAttachment]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[EmployeeAttachment](
	[EmployeeAttachmentId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[AttachmentTitle] [nvarchar](250) NULL,
	[AttachmentPath] [nvarchar](250) NULL,
 CONSTRAINT [PK_EmployeeAttachment] PRIMARY KEY CLUSTERED 
(
	[EmployeeAttachmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
