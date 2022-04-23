/****** Object:  Table [dbo].[RecruitmentApproval]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[RecruitmentApproval](
	[RecruitmentApprovalId] [bigint] IDENTITY(1,1) NOT NULL,
	[RecruitmentId] [bigint] NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[ApprovedDate] [datetime] NOT NULL,
	[Remarks] [nvarchar](450) NULL,
	[ForwardToEmployeeId] [bigint] NULL,
	[LookRecruitmentStatusId] [bigint] NULL,
 CONSTRAINT [PK_RecruitmentApproval] PRIMARY KEY CLUSTERED 
(
	[RecruitmentApprovalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
