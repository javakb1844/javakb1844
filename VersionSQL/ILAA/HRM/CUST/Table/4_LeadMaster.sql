/****** Object:  Table [CUST].[4_LeadMaster]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [CUST].[4_LeadMaster](
	[LID] [bigint] IDENTITY(1,1) NOT NULL,
	[LeadName] [varchar](500) NULL,
	[LeadNumber] [varchar](500) NULL,
	[LeadSourceId] [int] NULL,
	[LeadStatusId] [int] NOT NULL,
	[LeadEmail] [varchar](500) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedDateUTC] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NULL,
	[LastModifiedDateUTC] [datetime] NULL,
	[CreatedByLID] [bigint] NOT NULL,
	[LastModifiedByLID] [bigint] NOT NULL,
	[ProductSaleProfileId] [int] NOT NULL,
	[AsignToLID] [bigint] NOT NULL,
	[LeadCity] [varchar](1000) NULL,
 CONSTRAINT [PK_LeadMaster_4] PRIMARY KEY CLUSTERED 
(
	[LID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [CUST].[4_LeadMaster] ADD  CONSTRAINT [DF_4_LeadMaster_LeadSourceId]  DEFAULT ((1)) FOR [LeadSourceId]
ALTER TABLE [CUST].[4_LeadMaster] ADD  CONSTRAINT [DF_4_LeadMaster_LeadStatusId]  DEFAULT ((1)) FOR [LeadStatusId]
ALTER TABLE [CUST].[4_LeadMaster] ADD  CONSTRAINT [DF_LeadMaster_4_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
ALTER TABLE [CUST].[4_LeadMaster] ADD  CONSTRAINT [DF_LeadMaster_4_CreatedDateUTC]  DEFAULT (getutcdate()) FOR [CreatedDateUTC]
ALTER TABLE [CUST].[4_LeadMaster] ADD  CONSTRAINT [DF_LeadMaster_4_CreatedByLID]  DEFAULT ((0)) FOR [CreatedByLID]
ALTER TABLE [CUST].[4_LeadMaster] ADD  CONSTRAINT [DF_LeadMaster_4_LastModifiedByLID]  DEFAULT ((0)) FOR [LastModifiedByLID]
ALTER TABLE [CUST].[4_LeadMaster] ADD  CONSTRAINT [DF_LeadMaster_4_ProductSaleProfileId]  DEFAULT ((0)) FOR [ProductSaleProfileId]
ALTER TABLE [CUST].[4_LeadMaster] ADD  CONSTRAINT [DF_4_LeadMaster_AsignToLID]  DEFAULT ((0)) FOR [AsignToLID]
ALTER TABLE [CUST].[4_LeadMaster]  WITH CHECK ADD  CONSTRAINT [FK_LeadMaster_4_LeadSource] FOREIGN KEY([LeadSourceId])
REFERENCES [CUST].[4_LeadSource] ([LeadSourceId])
ALTER TABLE [CUST].[4_LeadMaster] CHECK CONSTRAINT [FK_LeadMaster_4_LeadSource]
ALTER TABLE [CUST].[4_LeadMaster]  WITH CHECK ADD  CONSTRAINT [FK_LeadMaster_4_LeadStatus] FOREIGN KEY([LeadStatusId])
REFERENCES [CUST].[4_LeadStatus] ([LeadStatusId])
ALTER TABLE [CUST].[4_LeadMaster] CHECK CONSTRAINT [FK_LeadMaster_4_LeadStatus]
