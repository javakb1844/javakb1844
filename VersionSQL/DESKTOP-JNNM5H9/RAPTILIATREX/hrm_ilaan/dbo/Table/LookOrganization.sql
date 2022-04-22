/****** Object:  Table [dbo].[LookOrganization]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[LookOrganization](
	[LookOrganizationId] [bigint] IDENTITY(1,1) NOT NULL,
	[OrganizationName] [varchar](250) NULL,
	[OrganizationURL] [varchar](350) NULL,
 CONSTRAINT [PK_LookOrganization] PRIMARY KEY CLUSTERED 
(
	[LookOrganizationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
