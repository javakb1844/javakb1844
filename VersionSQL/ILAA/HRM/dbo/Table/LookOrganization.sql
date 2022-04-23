/****** Object:  Table [dbo].[LookOrganization]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[LookOrganization](
	[LookOrganizationId] [bigint] IDENTITY(1,1) NOT NULL,
	[OrganizationName] [varchar](250) NULL,
	[OrganizationURL] [varchar](350) NULL,
	[LookCompanyStatusId] [int] NOT NULL,
	[ProductSaleProfileId] [int] NULL,
 CONSTRAINT [PK_LookOrganization] PRIMARY KEY CLUSTERED 
(
	[LookOrganizationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[LookOrganization] ADD  CONSTRAINT [DF_LookOrganization_LookCompanyStatusId]  DEFAULT ((0)) FOR [LookCompanyStatusId]
ALTER TABLE [dbo].[LookOrganization] ADD  CONSTRAINT [DF_LookOrganization_ProductSaleProfileId]  DEFAULT ((0)) FOR [ProductSaleProfileId]
