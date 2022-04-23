/****** Object:  Table [dbo].[BiometricSync]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[BiometricSync](
	[BiometricSyncId] [int] IDENTITY(1,1) NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDateTime] [datetime] NULL,
	[ProductSaleProfileId] [int] NULL,
	[BioMetricInfoId] [bigint] NULL,
 CONSTRAINT [PK_BiometricSync] PRIMARY KEY CLUSTERED 
(
	[BiometricSyncId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
