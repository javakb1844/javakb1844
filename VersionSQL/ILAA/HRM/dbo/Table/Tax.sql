/****** Object:  Table [dbo].[Tax]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[Tax](
	[TaxId] [bigint] IDENTITY(1,1) NOT NULL,
	[LimitName] [nvarchar](50) NULL,
	[StartLimit] [decimal](18, 2) NULL,
	[MaxLimit] [decimal](18, 2) NULL,
	[FixTax] [decimal](18, 2) NULL,
	[TaxPercentageToMaxLimit] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Tax] PRIMARY KEY CLUSTERED 
(
	[TaxId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
