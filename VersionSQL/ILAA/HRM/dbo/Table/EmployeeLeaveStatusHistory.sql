/****** Object:  Table [dbo].[EmployeeLeaveStatusHistory]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[EmployeeLeaveStatusHistory](
	[EmployeeLeaveStatusHistoryId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmplyeeLeaveId] [bigint] NOT NULL,
	[LeaveStatusId] [bigint] NOT NULL,
	[CreateDate] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
 CONSTRAINT [PK_EmployeeLeaveStatusHistory] PRIMARY KEY CLUSTERED 
(
	[EmployeeLeaveStatusHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
