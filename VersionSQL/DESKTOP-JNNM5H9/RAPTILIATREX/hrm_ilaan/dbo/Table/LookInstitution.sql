/****** Object:  Table [dbo].[LookInstitution]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[LookInstitution](
	[LookInstitutionId] [bigint] IDENTITY(1,1) NOT NULL,
	[InstitutionName] [nvarchar](250) NULL,
 CONSTRAINT [PK_LookInstitution] PRIMARY KEY CLUSTERED 
(
	[LookInstitutionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
