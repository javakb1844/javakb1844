/****** Object:  Table [dbo].[AuditTable]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[AuditTable](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[Event_Data] [xml] NOT NULL,
	[ChangedBy] [sysname] NOT NULL,
	[ChangedOn] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
