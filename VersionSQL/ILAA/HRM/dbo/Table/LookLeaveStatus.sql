/****** Object:  Table [dbo].[LookLeaveStatus]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[LookLeaveStatus](
	[LookLeaveStatusId] [bigint] NOT NULL,
	[RequestStatus] [varchar](50) NULL,
	[AccessGroupId] [int] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_LookLeaveStatus] PRIMARY KEY CLUSTERED 
(
	[LookLeaveStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
