/****** Object:  Table [dbo].[HRRecruitmentProcess]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[HRRecruitmentProcess](
	[HRRecruitmentProcessId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProcessId] [bigint] NULL,
	[RecruitmentId] [bigint] NULL,
 CONSTRAINT [PK_HRRecruitmentProcess] PRIMARY KEY CLUSTERED 
(
	[HRRecruitmentProcessId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
