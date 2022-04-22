/****** Object:  Table [dbo].[EmployeeAppliedLeave]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[EmployeeAppliedLeave](
	[EmployeeAppliedLeaveId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[LeaveTimeStart] [datetime] NULL,
	[LaveTimeEnd] [datetime] NULL,
	[ActualTimeIn] [datetime] NULL,
	[ActualTimOut] [datetime] NULL,
	[Status] [bigint] NOT NULL,
	[LookPolicyId] [bigint] NULL,
	[ForDate] [date] NULL,
	[Reason] [nvarchar](400) NULL,
	[HRRemarks] [nvarchar](400) NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdateDate] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_EmployeeAppliedLeave] PRIMARY KEY CLUSTERED 
(
	[EmployeeAppliedLeaveId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[EmployeeAppliedLeave] ADD  CONSTRAINT [DF_EmployeeAppliedLeave_IsLeave]  DEFAULT ((0)) FOR [Status]
