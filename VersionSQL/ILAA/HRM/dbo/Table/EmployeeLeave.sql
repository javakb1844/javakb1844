/****** Object:  Table [dbo].[EmployeeLeave]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[EmployeeLeave](
	[EmployeeLeaveId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeePolicyId] [bigint] NULL,
	[PolicyName] [nvarchar](100) NULL,
	[LeaveValue] [nvarchar](100) NULL,
	[LookLeaveStatusId] [bigint] NOT NULL,
	[LeaveDate] [datetime] NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[PolicyUnit] [nvarchar](50) NULL,
	[Year] [bigint] NULL,
	[LookPolicyId] [bigint] NULL,
	[EmployeeLeaveSummaryId] [bigint] NULL,
	[LookLeaveRequestStatusId] [bigint] NULL,
	[LookLeaveRequestDate] [datetime] NULL,
	[LeaveApprovedById] [bigint] NULL,
	[LeaveApprovedDate] [datetime] NULL,
 CONSTRAINT [PK_EmployeeLeave] PRIMARY KEY CLUSTERED 
(
	[EmployeeLeaveId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[EmployeeLeave] ADD  CONSTRAINT [DF_EmployeeLeave_LeaveStatus]  DEFAULT ((1)) FOR [LookLeaveStatusId]
