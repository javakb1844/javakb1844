/****** Object:  Table [CUST].[4_CustTableInfo]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [CUST].[4_CustTableInfo](
	[CustTableInfoLID] [bigint] IDENTITY(1,1) NOT NULL,
	[TableId] [int] NULL,
	[ColumnName] [varchar](800) NULL,
	[ParentName] [varchar](500) NULL,
	[ASCaption] [varchar](500) NULL,
	[IsVisible] [char](1) NOT NULL,
	[IsReadOnly] [int] NOT NULL,
	[IsCompulsory] [int] NOT NULL,
	[DataSelect] [varchar](3000) NULL,
	[ProductSaleProfileId] [int] NOT NULL,
	[LookColumnTypeId] [int] NULL,
	[TableName] [varchar](200) NULL,
	[ParentLID] [bigint] NULL,
	[ColumnId] [int] NULL,
	[QueryType] [varchar](500) NULL,
	[EmployeeId] [bigint] NOT NULL,
 CONSTRAINT [PK_4_CustTableInfo] PRIMARY KEY CLUSTERED 
(
	[CustTableInfoLID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [CUST].[4_CustTableInfo] ADD  CONSTRAINT [DF_4_CustTableInfo_TableId]  DEFAULT ((0)) FOR [TableId]
ALTER TABLE [CUST].[4_CustTableInfo] ADD  CONSTRAINT [DF_4_CustTableInfo_IsVisible]  DEFAULT ('a') FOR [IsVisible]
ALTER TABLE [CUST].[4_CustTableInfo] ADD  CONSTRAINT [DF_4_CustTableInfo_IsReadOnly]  DEFAULT ((0)) FOR [IsReadOnly]
ALTER TABLE [CUST].[4_CustTableInfo] ADD  CONSTRAINT [DF_4_CustTableInfo_IsCompulsory]  DEFAULT ((0)) FOR [IsCompulsory]
ALTER TABLE [CUST].[4_CustTableInfo] ADD  CONSTRAINT [DF_4_CustTableInfo_ProductSaleProfileId]  DEFAULT ((0)) FOR [ProductSaleProfileId]
ALTER TABLE [CUST].[4_CustTableInfo] ADD  CONSTRAINT [DF_4_CustTableInfo_ParentLID]  DEFAULT ((0)) FOR [ParentLID]
ALTER TABLE [CUST].[4_CustTableInfo] ADD  CONSTRAINT [DF_4_CustTableInfo_ColumnId]  DEFAULT ((0)) FOR [ColumnId]
ALTER TABLE [CUST].[4_CustTableInfo] ADD  CONSTRAINT [DF_4_CustTableInfo_EmployeeId]  DEFAULT ((0)) FOR [EmployeeId]
