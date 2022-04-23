/****** Object:  Table [dbo].[LookColumnType]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[LookColumnType](
	[LookColumnTypeId] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [varchar](50) NOT NULL,
	[SQLTypeName] [varchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_LookColumnType] PRIMARY KEY CLUSTERED 
(
	[LookColumnTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
