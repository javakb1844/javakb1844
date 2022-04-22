/****** Object:  Table [dbo].[LogInHistory]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[LogInHistory](
	[LogInId] [bigint] IDENTITY(1,1) NOT NULL,
	[IP] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[Query] [nvarchar](1000) NULL,
 CONSTRAINT [PK_LogInHistory] PRIMARY KEY CLUSTERED 
(
	[LogInId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
