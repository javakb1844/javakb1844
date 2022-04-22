/****** Object:  Table [dbo].[PayRoll]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[PayRoll](
	[PayRollId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[MonthNumber] [bigint] NOT NULL,
	[YearNumber] [bigint] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[GrossPay] [decimal](18, 2) NULL,
	[NetPay] [decimal](18, 2) NULL,
	[IncomeTax] [decimal](18, 2) NULL,
	[WorkingDaysRequired] [bigint] NULL,
	[PublicHolidays] [bigint] NULL,
	[Leaves] [bigint] NULL,
	[Absents] [bigint] NULL,
	[Attendance] [bigint] NULL,
	[TotalHoursRequired] [bigint] NULL,
	[TotalHours] [bigint] NULL,
	[LookPayRollStatusId] [bigint] NULL,
	[StatusUpdatedBy] [bigint] NULL,
	[StatusUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_PayRoll] PRIMARY KEY CLUSTERED 
(
	[PayRollId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[PayRoll] ADD  CONSTRAINT [DF_PayRoll_LookPayRollStatusId]  DEFAULT ((0)) FOR [LookPayRollStatusId]
