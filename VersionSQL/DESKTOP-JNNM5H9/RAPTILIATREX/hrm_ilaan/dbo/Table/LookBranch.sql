/****** Object:  Table [dbo].[LookBranch]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[LookBranch](
	[LookBranchId] [bigint] IDENTITY(1,1) NOT NULL,
	[BranchName] [nvarchar](250) NULL,
	[LookBranchTypeId] [bigint] NULL,
	[ParentBranchId] [bigint] NULL,
 CONSTRAINT [PK_LookBranch] PRIMARY KEY CLUSTERED 
(
	[LookBranchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
