/****** Object:  Table [dbo].[ApplicantInterview]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[ApplicantInterview](
	[ApplicantInterviewId] [bigint] IDENTITY(1,1) NOT NULL,
	[MeetingTime] [time](7) NULL,
	[ApplicantId] [bigint] NULL,
	[Points] [bigint] NULL,
	[Status] [bigint] NULL,
	[CreateDate] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[MeetingDate] [datetime] NULL,
 CONSTRAINT [PK_ApplicantInterview] PRIMARY KEY CLUSTERED 
(
	[ApplicantInterviewId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
