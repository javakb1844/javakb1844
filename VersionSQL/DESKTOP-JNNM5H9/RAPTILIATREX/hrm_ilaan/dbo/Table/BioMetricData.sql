/****** Object:  Table [dbo].[BioMetricData]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[BioMetricData](
	[BioMetricDataId] [int] IDENTITY(1,1) NOT NULL,
	[BioMetricId] [int] NOT NULL,
	[DateTimeRecord] [datetime] NOT NULL,
	[DateOnly] [date] NULL,
	[TimeOnly] [datetime] NULL,
	[dwInOutMode] [int] NULL,
 CONSTRAINT [PK_BioMetricData] PRIMARY KEY CLUSTERED 
(
	[BioMetricDataId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
