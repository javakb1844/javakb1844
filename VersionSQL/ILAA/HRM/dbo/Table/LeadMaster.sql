/****** Object:  Table [dbo].[LeadMaster]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[LeadMaster](
	[LeadId] [bigint] IDENTITY(1,1) NOT NULL,
	[ID] [bigint] NOT NULL,
	[LeadName] [varchar](500) NULL,
	[LeadNumber] [varchar](500) NULL,
	[LeadSourceId] [bigint] NULL,
	[LeadStatusId] [bigint] NULL,
	[LeadEmail] [varchar](500) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedDateUTC] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NULL,
	[LastModifiedDateUTC] [datetime] NULL,
	[CreatedBy] [bigint] NOT NULL,
	[LastModifiedBy] [bigint] NOT NULL,
	[ProductSaleProfileId] [int] NOT NULL,
 CONSTRAINT [PK_LeadMaster] PRIMARY KEY CLUSTERED 
(
	[LeadId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[LeadMaster] ADD  CONSTRAINT [DF_LeadMaster_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
ALTER TABLE [dbo].[LeadMaster] ADD  CONSTRAINT [DF_LeadMaster_CreatedDateUTC]  DEFAULT (getutcdate()) FOR [CreatedDateUTC]
ALTER TABLE [dbo].[LeadMaster] ADD  CONSTRAINT [DF_LeadMaster_CreatedBy]  DEFAULT ((0)) FOR [CreatedBy]
ALTER TABLE [dbo].[LeadMaster] ADD  CONSTRAINT [DF_LeadMaster_LastModifiedBy]  DEFAULT ((0)) FOR [LastModifiedBy]
ALTER TABLE [dbo].[LeadMaster] ADD  CONSTRAINT [DF_LeadMaster_ProductSaleProfileId]  DEFAULT ((0)) FOR [ProductSaleProfileId]
