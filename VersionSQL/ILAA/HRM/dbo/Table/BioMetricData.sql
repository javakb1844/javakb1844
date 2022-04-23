/****** Object:  Table [dbo].[BioMetricData]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[BioMetricData](
	[BioMetricDataId] [bigint] IDENTITY(1,1) NOT NULL,
	[BioMetricId] [int] NOT NULL,
	[DateTimeRecord] [datetime] NOT NULL,
	[OnBioMetricName] [varchar](1000) NULL,
	[DateOnly] [date] NULL,
	[TimeOnly] [datetime] NULL,
	[dwInOutMode] [int] NULL,
	[IsEdit] [bit] NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdateDate] [datetime] NULL,
	[EmployeeId] [bigint] NULL,
	[ProductSaleProfileId] [int] NULL,
	[BioMetricSerialNo] [varchar](300) NULL,
	[BioMetricDeviceBrand] [varchar](300) NULL,
	[BioMetricInfoId] [bigint] NULL,
 CONSTRAINT [PK_BioMetricData] PRIMARY KEY CLUSTERED 
(
	[BioMetricDataId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[BioMetricData] ADD  CONSTRAINT [DF_BioMetricData_IsEdit]  DEFAULT ((0)) FOR [IsEdit]
