/****** Object:  Table [dbo].[ApplicantAttachment]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[ApplicantAttachment](
	[ApplicantAttachmentId] [bigint] IDENTITY(1,1) NOT NULL,
	[AttachmentTitle] [varchar](250) NULL,
	[AttachmentPath] [varchar](250) NULL,
	[ApplicantId] [bigint] NOT NULL,
 CONSTRAINT [PK_ApplicantAttachment] PRIMARY KEY CLUSTERED 
(
	[ApplicantAttachmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
