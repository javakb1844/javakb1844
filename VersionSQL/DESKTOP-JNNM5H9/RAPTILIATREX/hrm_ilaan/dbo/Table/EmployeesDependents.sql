/****** Object:  Table [dbo].[EmployeesDependents]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[EmployeesDependents](
	[EmployeesDependentId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [bigint] NULL,
	[Name] [nvarchar](250) NULL,
	[Relation] [nvarchar](250) NULL,
	[Gender] [char](10) NULL,
 CONSTRAINT [PK_EmployeesDependents] PRIMARY KEY CLUSTERED 
(
	[EmployeesDependentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
