/****** Object:  Table [dbo].[BioMetricInfo]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[BioMetricInfo](
	[BioMetricInfoId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductSaleProfileId] [int] NULL,
	[BioMetricSerialNo] [varchar](300) NULL,
	[BioMetricDeviceBrand] [varchar](300) NULL,
	[CreateDate] [datetime] NULL,
	[IPAddress] [varchar](300) NULL,
	[IsPublicIp] [bit] NULL
) ON [PRIMARY]

ALTER TABLE [dbo].[BioMetricInfo] ADD  CONSTRAINT [DF_BioMetricInfoId_IsPublicIp]  DEFAULT ((0)) FOR [IsPublicIp]
