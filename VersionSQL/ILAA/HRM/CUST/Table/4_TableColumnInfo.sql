/****** Object:  Table [CUST].[4_TableColumnInfo]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [CUST].[4_TableColumnInfo](
	[TableColumnInfoLId] [bigint] IDENTITY(1,1) NOT NULL,
	[ColumnName] [varchar](200) NULL,
	[ColumnNameDB] [varchar](200) NULL,
	[ParentName] [varchar](500) NULL,
	[ASCaption] [varchar](500) NULL,
	[TableName] [varchar](500) NULL,
	[ProductSaleProfileId] [int] NOT NULL,
	[ColumnCatId] [int] NULL,
	[ColumnType] [varchar](300) NULL,
	[CreateDate] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[ColumnStatudId] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[ParentLId] [bigint] NULL,
	[ForginKeyTableName] [varchar](500) NULL,
 CONSTRAINT [PK_TableColumnInfo] PRIMARY KEY CLUSTERED 
(
	[TableColumnInfoLId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [CUST].[4_TableColumnInfo] ADD  CONSTRAINT [DF_TableColumnInfo_4_ProductSaleProfileId]  DEFAULT ((0)) FOR [ProductSaleProfileId]
ALTER TABLE [CUST].[4_TableColumnInfo] ADD  CONSTRAINT [DF_TableColumnInfo_4_ColumnCatId]  DEFAULT ((0)) FOR [ColumnCatId]
ALTER TABLE [CUST].[4_TableColumnInfo] ADD  CONSTRAINT [DF_TableColumnInfo_4_ColumnStatudId]  DEFAULT ((0)) FOR [ColumnStatudId]
ALTER TABLE [CUST].[4_TableColumnInfo] ADD  CONSTRAINT [DF_TableColumnInfo_4_ParentLId]  DEFAULT ((0)) FOR [ParentLId]
