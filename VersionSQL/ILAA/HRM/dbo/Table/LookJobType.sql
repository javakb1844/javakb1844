/****** Object:  Table [dbo].[LookJobType]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[LookJobType](
	[LookJobTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[JobTypeName] [nvarchar](150) NULL,
 CONSTRAINT [PK_LookJobType] PRIMARY KEY CLUSTERED 
(
	[LookJobTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
