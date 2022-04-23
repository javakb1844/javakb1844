/****** Object:  Table [dbo].[ProductSaleProfile]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[ProductSaleProfile](
	[ProductSaleProfileId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NULL,
	[LookCityId] [bigint] NULL,
	[LookCountryId] [bigint] NULL,
	[Address] [varchar](1000) NULL,
	[LookCustomerStatusId] [int] NOT NULL,
	[RegistrationDatePk] [datetime] NULL,
	[UITCDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductSaleProfile] PRIMARY KEY CLUSTERED 
(
	[ProductSaleProfileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[ProductSaleProfile] ADD  CONSTRAINT [DF_ProductSaleProfile_LookCustomerStatusId]  DEFAULT ((0)) FOR [LookCustomerStatusId]
