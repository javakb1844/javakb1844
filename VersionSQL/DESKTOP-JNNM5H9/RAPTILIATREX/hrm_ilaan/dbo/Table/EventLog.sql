/****** Object:  Table [dbo].[EventLog]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[EventLog](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[ControllerName] [nvarchar](100) NULL,
	[ActionName] [nvarchar](100) NULL,
	[Query] [ntext] NULL,
	[CreateDate] [timestamp] NOT NULL,
	[AppID] [bigint] NULL,
	[InsertDate] [datetime] NULL,
	[ProjectName] [nvarchar](100) NULL,
	[UserId] [bigint] NULL,
 CONSTRAINT [PK_EventLog] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
