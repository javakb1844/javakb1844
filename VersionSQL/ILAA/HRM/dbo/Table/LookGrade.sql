/****** Object:  Table [dbo].[LookGrade]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[LookGrade](
	[LookGradeId] [bigint] IDENTITY(1,1) NOT NULL,
	[GradeName] [varchar](50) NULL,
	[GradeValue] [decimal](18, 1) NULL,
	[ProductSaleProfileId] [int] NULL,
 CONSTRAINT [PK_LookGrade] PRIMARY KEY CLUSTERED 
(
	[LookGradeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
