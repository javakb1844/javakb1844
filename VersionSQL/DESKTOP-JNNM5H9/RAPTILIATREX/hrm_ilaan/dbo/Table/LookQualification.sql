/****** Object:  Table [dbo].[LookQualification]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[LookQualification](
	[LookQualificationId] [bigint] IDENTITY(1,1) NOT NULL,
	[QualificationName] [nvarchar](100) NULL,
	[LookQualificationTypeId] [bigint] NULL,
 CONSTRAINT [PK_LookQualification] PRIMARY KEY CLUSTERED 
(
	[LookQualificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
