/****** Object:  Table [dbo].[LookQualificationType]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[LookQualificationType](
	[LookQualificationTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[QualificationType] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_LookQualificationType] PRIMARY KEY CLUSTERED 
(
	[LookQualificationTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
