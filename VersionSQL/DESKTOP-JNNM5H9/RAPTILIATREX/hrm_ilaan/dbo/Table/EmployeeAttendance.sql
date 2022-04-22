/****** Object:  Table [dbo].[EmployeeAttendance]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[EmployeeAttendance](
	[EmployeeAttendanceId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[TimeIn] [datetime] NULL,
	[TimeOut] [datetime] NULL,
	[ActualTimeIn] [datetime] NULL,
	[ActualTimOut] [datetime] NULL,
	[Status] [bigint] NOT NULL,
	[LookPolicyId] [bigint] NULL,
	[ForDate] [date] NULL,
 CONSTRAINT [PK_EmployeeAttendance] PRIMARY KEY CLUSTERED 
(
	[EmployeeAttendanceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[EmployeeAttendance] ADD  CONSTRAINT [DF_Table_1_IsLeave]  DEFAULT ((0)) FOR [Status]
