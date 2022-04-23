/****** Object:  Table [dbo].[EmployeeLeaveSummary]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[EmployeeLeaveSummary](
	[EmployeeLeaveSummaryId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeePolicyId] [bigint] NOT NULL,
	[StartLeaveDate] [datetime] NOT NULL,
	[EndLeaveDate] [datetime] NOT NULL,
	[ShortLeaves] [bigint] NULL,
	[HalfLeaves] [bigint] NULL,
	[Leaves] [bigint] NULL,
	[EmployeeId] [bigint] NOT NULL,
	[LookLeaveStatusId] [bigint] NULL,
	[CreateDate] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[StatusUpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_EmployeeLeaveSummary] PRIMARY KEY CLUSTERED 
(
	[EmployeeLeaveSummaryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[EmployeeLeaveSummary] ADD  CONSTRAINT [DF_EmployeeLeaveSummary_LookLeaveStatusId]  DEFAULT ((1)) FOR [LookLeaveStatusId]
