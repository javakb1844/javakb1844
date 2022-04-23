/****** Object:  Table [dbo].[PayRollSummary]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[PayRollSummary](
	[PayRollSummaryId] [bigint] IDENTITY(1,1) NOT NULL,
	[YearNumber] [int] NOT NULL,
	[MonthNumber] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[SalaryCount] [bigint] NOT NULL,
 CONSTRAINT [PK_PayRollSummaryI] PRIMARY KEY CLUSTERED 
(
	[PayRollSummaryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
