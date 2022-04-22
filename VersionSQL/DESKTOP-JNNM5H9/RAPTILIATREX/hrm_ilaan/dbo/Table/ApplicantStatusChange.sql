/****** Object:  Table [dbo].[ApplicantStatusChange]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[ApplicantStatusChange](
	[ApplicantStatusChangeId] [bigint] IDENTITY(1,1) NOT NULL,
	[LookApplicantStatusId] [bigint] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Comments] [nvarchar](500) NULL,
	[ApplicantId] [bigint] NOT NULL,
 CONSTRAINT [PK_ApplicantStatusChange] PRIMARY KEY CLUSTERED 
(
	[ApplicantStatusChangeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
