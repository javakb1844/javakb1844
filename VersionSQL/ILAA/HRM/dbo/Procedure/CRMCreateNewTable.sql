/****** Object:  Procedure [dbo].[CRMCreateNewTable]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CRMCreateNewTable]
	-- Add the parameters for the stored procedure here
	@ProductSaleProfileId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SET ANSI_NULLS ON


SET QUOTED_IDENTIFIER ON
declare @vari varchar(max)
BEGIN TRY
set @vari='CREATE TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_TableColumnInfo](
	[TableColumnInfoLId]  [bigint] IDENTITY(1,1) NOT NULL,
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
	[ParentLId]  [bigint] NULL,
	[ForginKeyTableName] [varchar](500) NULL
	 CONSTRAINT [PK_TableColumnInfo] PRIMARY KEY CLUSTERED 
(
	[TableColumnInfoLId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
ALTER TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_TableColumnInfo] ADD  CONSTRAINT [DF_TableColumnInfo_'+cast (@ProductSaleProfileId as varchar(3))+'_ColumnCatId]  DEFAULT ((0)) FOR [ColumnCatId]
ALTER TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_TableColumnInfo] ADD  CONSTRAINT [DF_TableColumnInfo_'+cast (@ProductSaleProfileId as varchar(3))+'_ColumnStatudId]  DEFAULT ((0)) FOR [ColumnStatudId]
ALTER TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_TableColumnInfo] ADD  CONSTRAINT [DF_TableColumnInfo_'+cast (@ProductSaleProfileId as varchar(3))+'_ProductSaleProfileId]  DEFAULT ((0)) FOR [ProductSaleProfileId]
ALTER TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_TableColumnInfo] ADD  CONSTRAINT [DF_TableColumnInfo_'+cast (@ProductSaleProfileId as varchar(3))+'_ParentLId]  DEFAULT ((0)) FOR [ParentLId]';
 exec(@vari)	


set @vari='CREATE TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadStatus](
	[LeadStatusId] [int] IDENTITY(1,1) NOT NULL,
	[LeadStatus] [varchar](200) NOT NULL,
 CONSTRAINT [PK_LeadStatus_'+cast (@ProductSaleProfileId as varchar(3))+'] PRIMARY KEY CLUSTERED 
(
	[LeadStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]';
  --exec(@vari)

  



  set @vari='INSERT INTO [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadStatus] ([LeadStatus])VALUES(''working'')';
  -- exec(@vari)

   set @vari='CREATE TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadSource](
	[LeadSourceId] [int] IDENTITY(1,1) NOT NULL,
	[LeadSource] [varchar](200) NOT NULL,
 CONSTRAINT [PK_LeadSource_'+cast (@ProductSaleProfileId as varchar(3))+'] PRIMARY KEY CLUSTERED 
(
	[LeadSourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]';
  exec(@vari)




  set @vari='INSERT INTO [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadSource] ([LeadSource])VALUES(''Facebook'')';
   exec(@vari)
 set @vari='CREATE TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster](
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
 CONSTRAINT [PK_LeadMaster_'+cast (@ProductSaleProfileId as varchar(3))+'] PRIMARY KEY CLUSTERED 
(
	[LID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
ALTER TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster] ADD  CONSTRAINT [DF_'+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster_LeadSourceId]  DEFAULT ((1)) FOR [LeadSourceId]
ALTER TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster] ADD  CONSTRAINT [DF_'+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster_LeadStatusId]  DEFAULT ((1)) FOR [LeadStatusId]
ALTER TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster] ADD  CONSTRAINT [DF_LeadMaster_'+cast (@ProductSaleProfileId as varchar(3))+'_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
ALTER TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster] ADD  CONSTRAINT [DF_LeadMaster_'+cast (@ProductSaleProfileId as varchar(3))+'_CreatedDateUTC]  DEFAULT (getutcdate()) FOR [CreatedDateUTC]
ALTER TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster] ADD  CONSTRAINT [DF_LeadMaster_'+cast (@ProductSaleProfileId as varchar(3))+'_CreatedByLID]  DEFAULT ((0)) FOR [CreatedByLID]
ALTER TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster] ADD  CONSTRAINT [DF_LeadMaster_'+cast (@ProductSaleProfileId as varchar(3))+'_LastModifiedByLID]  DEFAULT ((0)) FOR [LastModifiedByLID]
ALTER TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster] ADD  CONSTRAINT [DF_LeadMaster_'+cast (@ProductSaleProfileId as varchar(3))+'_ProductSaleProfileId]  DEFAULT ((0)) FOR [ProductSaleProfileId]
ALTER TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster] ADD  CONSTRAINT [DF_'+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster_AsignToLID]  DEFAULT ((0)) FOR [AsignToLID]
ALTER TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]  WITH CHECK ADD  CONSTRAINT [FK_LeadMaster_'+cast (@ProductSaleProfileId as varchar(3))+'_LeadSource] FOREIGN KEY([LeadSourceId])
REFERENCES [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadSource] ([LeadSourceId])
ALTER TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster] CHECK CONSTRAINT [FK_LeadMaster_'+cast (@ProductSaleProfileId as varchar(3))+'_LeadSource] 
 


ALTER TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]  WITH CHECK ADD  CONSTRAINT [FK_LeadMaster_'+cast (@ProductSaleProfileId as varchar(3))+'_LeadStatus] FOREIGN KEY([LeadStatusId])
REFERENCES [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadStatus] ([LeadStatusId])
ALTER TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster] CHECK CONSTRAINT [FK_LeadMaster_'+cast (@ProductSaleProfileId as varchar(3))+'_LeadStatus]' 
 
 exec(@vari)	

 --declare @vari varchar(max)
-- declare @ProductSaleProfileId int=4
 set @vari='CREATE TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo](
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
	[ColumnId] [int]  null,
	[QueryType] [varchar](500) NULL,
	[EmployeeId] [bigint] NOT NULL,
 CONSTRAINT [PK_'+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] PRIMARY KEY CLUSTERED 
(
	[CustTableInfoLID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
ALTER TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ADD  CONSTRAINT [DF_'+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo_ColumnId]  DEFAULT ((0)) FOR [ColumnId]
ALTER TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ADD  CONSTRAINT [DF_'+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo_TableId]  DEFAULT ((0)) FOR [TableId]
ALTER TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ADD  CONSTRAINT [DF_'+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo_IsVisible]  DEFAULT (''a'') FOR [IsVisible]
ALTER TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ADD  CONSTRAINT [DF_'+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo_IsReadOnly]  DEFAULT ((0)) FOR [IsReadOnly]
ALTER TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ADD  CONSTRAINT [DF_'+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo_IsCompulsory]  DEFAULT ((0)) FOR [IsCompulsory]
ALTER TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ADD  CONSTRAINT [DF_'+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo_ProductSaleProfileId]  DEFAULT ((0)) FOR [ProductSaleProfileId]
ALTER TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ADD  CONSTRAINT [DF_'+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo_ParentLID]  DEFAULT ((0)) FOR [ParentLID]
ALTER TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ADD  CONSTRAINT [DF_'+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo_EmployeeId]  DEFAULT ((0)) FOR [EmployeeId]'


exec(@vari)	

 --declare @vari varchar(max)
 --declare @ProductSaleProfileId int=4
-- set @vari='SET IDENTITY_INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ON 
--INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ([CustTableInfoLID],[ColumnId], [TableId], [ColumnName], [ParentName], [ASCaption], [IsVisible], [DataSelect], [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (1, 1, 1, N''[lm].[LID]'', NULL, N''LID'', N''v'', NULL, 4, 3, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
--INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ([CustTableInfoLID], [ColumnId],[TableId], [ColumnName], [ParentName], [ASCaption], [IsVisible], [DataSelect], [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (2, 2, 1, N''[lm].[LeadName]'', NULL, N''LeadName'', N''v'', NULL, 4, 0, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
--INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ([CustTableInfoLID], [ColumnId],[TableId], [ColumnName], [ParentName], [ASCaption], [IsVisible], [DataSelect], [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (3, 3, 1, N''[lm].[LeadNumber]'', NULL, N''LeadNumber'', N''v'', NULL, 4, 0, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
--INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ([CustTableInfoLID], [ColumnId],[TableId], [ColumnName], [ParentName], [ASCaption], [IsVisible], [DataSelect], [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (4, 4, 1, N''[lm].[LeadEmail]'', NULL, N''LeadEmail'', N''a'', NULL, 4, 0, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
--INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ([CustTableInfoLID], [ColumnId],[TableId], [ColumnName], [ParentName], [ASCaption], [IsVisible], [DataSelect], [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (5, 5, 1, N''[lm].[CreatedDate]'', NULL, N''CreatedDate'', N''a'', NULL, 4, 4, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
--INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ([CustTableInfoLID], [ColumnId],[TableId], [ColumnName], [ParentName], [ASCaption], [IsVisible], [DataSelect], [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (6, 6, 1, N''[lm].[CreatedDateUTC]'', NULL, N''CreatedDateUTC'', N''v'', NULL, 4, 4, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
--INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ([CustTableInfoLID], [ColumnId],[TableId], [ColumnName], [ParentName], [ASCaption], [IsVisible], [DataSelect], [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (7, 7, 1, N''[lm].[LastModifiedDate]'', NULL, N''LastModifiedDate'', N''a'', NULL, 4, 4, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
--INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ([CustTableInfoLID], [ColumnId],[TableId], [ColumnName], [ParentName], [ASCaption], [IsVisible], [DataSelect], [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (8, 8, 1, N''[lm].[LastModifiedDateUTC]'', NULL, N''LastModifiedDateUTC'', N''v'', NULL, 4, 4, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
--INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ([CustTableInfoLID], [ColumnId],[TableId], [ColumnName], [ParentName], [ASCaption], [IsVisible], [DataSelect], [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (9, 9, 1, N''[lm].[CreatedByLID]'', N''CreatedByLID/'', N''LID'', N''v'', N'''', 4, 9, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
--INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ([CustTableInfoLID], [ColumnId],[TableId], [ColumnName], [ParentName], [ASCaption], [IsVisible], [DataSelect], [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (10,10, 1, N''(SELECT   fullname  FROM dbo.employee where employeeid=[lm].[CreatedByLID]  ) '', N''CreatedByLID/'', N''Name'', N''v'', NULL, 4, 0, NULL, 9, N''LeadSearch'', 0)
--INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ([CustTableInfoLID], [ColumnId],[TableId], [ColumnName], [ParentName], [ASCaption], [IsVisible], [DataSelect], [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (11,11, 1, N''[lm].[LastModifiedByLID]'', N''LastModifiedByLID/'', N''LID'', N''v'', N'''', 4, 9, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
--INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ([CustTableInfoLID], [ColumnId],[TableId], [ColumnName], [ParentName], [ASCaption], [IsVisible], [DataSelect], [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (12,12, 1, N''(SELECT   fullname  FROM dbo.employee where employeeid=[lm].[LastModifiedByLID]  ) '', N''LastModifiedByLID/'', N''Name'', N''v'', NULL, 4, 0, NULL, 11, N''LeadSearch'', 0)
--INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ([CustTableInfoLID], [ColumnId],[TableId], [ColumnName], [ParentName], [ASCaption], [IsVisible], [DataSelect], [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (13,13, 1, N''[lm].[AsignToLID]'', N''AsignToLID/'', N''LID'', N''v'', N'''', 4, 9, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
--INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ([CustTableInfoLID], [ColumnId],[TableId], [ColumnName], [ParentName], [ASCaption], [IsVisible], [DataSelect], [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (14,14, 1, N'' (SELECT   fullname  FROM dbo.employee where employeeid=[lm].[AsignToLID]  ) '', N''AsignToLID/'', N''Name'', N''v'', NULL, 4, 0, NULL, 13, N''LeadSearch'', 0)
--INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ([CustTableInfoLID], [ColumnId],[TableId], [ColumnName], [ParentName], [ASCaption], [IsVisible], [DataSelect], [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (15,15, 1, N''[lm].[LeadSourceId]'', N''LeadSourceId/'', N''Id'', N''v'', N'''', 4, 6, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
--INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ([CustTableInfoLID], [ColumnId],[TableId], [ColumnName], [ParentName], [ASCaption], [IsVisible], [DataSelect], [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (16,16, 1, N''(SELECT   [LeadSourceId],LeadSource   FROM cust.['+cast (@ProductSaleProfileId as varchar(3))+'_LeadSource]   FOR JSON AUTO) '', N''LeadSourceId/'', N''Data'', N''v'', NULL, 4, 0, NULL, 15, N''LeadSearch'', 0)
--INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ([CustTableInfoLID], [ColumnId],[TableId], [ColumnName], [ParentName], [ASCaption], [IsVisible], [DataSelect], [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (17,17, 1, N''(SELECT   LeadSource  FROM cust.['+cast (@ProductSaleProfileId as varchar(3))+'_LeadSource] where LeadSourceId=[lm].[LeadSourceId]  )'', N''LeadSourceId/'', N''Name'', N''v'', NULL, 4, 0, NULL, 15, N''LeadSearch'', 0)
--INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ([CustTableInfoLID], [ColumnId],[TableId], [ColumnName], [ParentName], [ASCaption], [IsVisible], [DataSelect], [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (18,18, 1, N''[lm].[LeadStatusId]'', N''LeadStatusId/'', N''Id'', N''v'', N'''', 4, 6, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
--INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ([CustTableInfoLID], [ColumnId],[TableId], [ColumnName], [ParentName], [ASCaption], [IsVisible], [DataSelect], [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (19,19, 1, N''(SELECT   [LeadStatusId],LeadStatus   FROM cust.['+cast (@ProductSaleProfileId as varchar(3))+'_LeadStatus]   FOR JSON AUTO) '', N''LeadStatusId/'', N''Data'', N''v'', NULL, 4, 0, NULL, 18, N''LeadSearch'', 0)
--INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ([CustTableInfoLID], [ColumnId],[TableId], [ColumnName], [ParentName], [ASCaption], [IsVisible], [DataSelect], [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (20,20, 1, N''(SELECT   LeadStatus  FROM cust.['+cast (@ProductSaleProfileId as varchar(3))+'_LeadStatus] where LeadStatusId=[lm].[LeadStatusId]  )'', N''LeadStatusId/'', N''Name'', N''v'', NULL, 4, 0, NULL, 18, N''LeadSearch'', 0)
--SET IDENTITY_INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] OFF'
--exec(@vari)
-- declare @vari varchar(max)
 --declare @ProductSaleProfileId int=4   0,0,
set @vari='
INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ([ColumnId], [TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (1, 1, 1,0, N''[lm].[LID]'', NULL, N''LID'',										 N''v'','+cast (@ProductSaleProfileId as varchar(3))+', 3, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (2, 1, 0,1, N''[lm].[LeadName]'', NULL, N''LeadName'',							 N''v'','+cast (@ProductSaleProfileId as varchar(3))+', 0, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (3, 1, 0,1, N''[lm].[LeadNumber]'', NULL, N''LeadNumber'',						 N''v'','+cast (@ProductSaleProfileId as varchar(3))+', 0, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (4, 1, 0,0, N''[lm].[LeadEmail]'', NULL, N''LeadEmail'',							 N''a'','+cast (@ProductSaleProfileId as varchar(3))+', 0, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (5, 1, 1,0, N''[lm].[CreatedDate]'', NULL, N''CreatedDate'',					     N''a'','+cast (@ProductSaleProfileId as varchar(3))+', 4, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (6, 1, 1,0, N''[lm].[CreatedDateUTC]'', NULL, N''CreatedDateUTC'',				 N''v'','+cast (@ProductSaleProfileId as varchar(3))+', 4, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (7, 1, 1,0, N''[lm].[LastModifiedDate]'', NULL, N''LastModifiedDate'',			 N''a'','+cast (@ProductSaleProfileId as varchar(3))+', 4, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (8, 1, 1,0, N''[lm].[LastModifiedDateUTC]'', NULL, N''LastModifiedDateUTC'',	     N''v'','+cast (@ProductSaleProfileId as varchar(3))+', 4, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (9, 1, 1,0, N''[lm].[CreatedByLID]'', N''CreatedByLID/'', N''LID'',				 N''v'','+cast (@ProductSaleProfileId as varchar(3))+', 9, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (10, 1, 0,0, N''(SELECT   fullname  FROM dbo.employee where employeeid=[lm].[CreatedByLID]  ) '', N''CreatedByLID/'', N''Name'',				 N''v'', '+cast (@ProductSaleProfileId as varchar(3))+', 0, NULL, 9, N''LeadSearch'', 0)
INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (11, 1, 1,0, N''[lm].[LastModifiedByLID]'', N''LastModifiedByLID/'', N''LID'',																 N''v'', '+cast (@ProductSaleProfileId as varchar(3))+', 9, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (12, 1, 0,0, N''(SELECT   fullname  FROM dbo.employee where employeeid=[lm].[LastModifiedByLID]  ) '', N''LastModifiedByLID/'', N''Name'',    N''v'', '+cast (@ProductSaleProfileId as varchar(3))+', 0, NULL, 11, N''LeadSearch'', 0)
INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (13, 1, 0,0, N''[lm].[AsignToLID]'', N''AsignToLID/'', N''LID'',																			     N''v'', '+cast (@ProductSaleProfileId as varchar(3))+', 9, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (14, 1, 0,0, N'' (SELECT   fullname  FROM dbo.employee where employeeid=[lm].[AsignToLID]  ) '', N''AsignToLID/'', N''Name'',				 N''v'', '+cast (@ProductSaleProfileId as varchar(3))+', 0, NULL, 13, N''LeadSearch'', 0)
INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (15, 1, 0,0, N''[lm].[LeadSourceId]'', N''LeadSourceId/'', N''Id'',																			 N''v'', '+cast (@ProductSaleProfileId as varchar(3))+', 6, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (16, 1, 0,0, N''(SELECT   [LeadSourceId],LeadSource   FROM cust.['+cast (@ProductSaleProfileId as varchar(3))+'_LeadSource]   FOR JSON AUTO) '', N''LeadSourceId/'', N''Data'',				 N''v'', '+cast (@ProductSaleProfileId as varchar(3))+', 0, NULL, 15, N''LeadSearch'', 0)
INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (17, 1, 0,0, N''(SELECT   LeadSource  FROM cust.['+cast (@ProductSaleProfileId as varchar(3))+'_LeadSource] where LeadSourceId=[lm].[LeadSourceId]  )'', N''LeadSourceId/'', N''Name'',		 N''v'', '+cast (@ProductSaleProfileId as varchar(3))+', 0, NULL, 15, N''LeadSearch'', 0)
INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (18, 1, 0,0, N''[lm].[LeadStatusId]'', N''LeadStatusId/'', N''Id'',																															 N''v'', '+cast (@ProductSaleProfileId as varchar(3))+', 6, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (19, 1, 0,0, N''(SELECT   [LeadStatusId],LeadStatus   FROM cust.['+cast (@ProductSaleProfileId as varchar(3))+'_LeadStatus]   FOR JSON AUTO) '', N''LeadStatusId/'', N''Data'',				 N''v'', '+cast (@ProductSaleProfileId as varchar(3))+', 0, NULL, 18, N''LeadSearch'', 0)
INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (20, 1, 0,0, N''(SELECT   LeadStatus  FROM cust.['+cast (@ProductSaleProfileId as varchar(3))+'_LeadStatus] where LeadStatusId=[lm].[LeadStatusId]  )'', N''LeadStatusId/'', N''Name'',		 N''v'', '+cast (@ProductSaleProfileId as varchar(3))+', 0, NULL, 18, N''LeadSearch'', 0)
';
exec(@vari)
 --declare @vari varchar(max)
 --declare @ProductSaleProfileId int=4
set @vari= 'INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ([ColumnId], [TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (1, 2, 1,0, N''[lm].[LID]'', NULL, N''LID'',										 N''v'','+cast (@ProductSaleProfileId as varchar(3))+', 3, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
			INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (2, 2, 0,1, N''[lm].[LeadName]'', NULL, N''LeadName'',							 N''v'','+cast (@ProductSaleProfileId as varchar(3))+', 0, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
			INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (3, 2, 0,1, N''[lm].[LeadNumber]'', NULL, N''LeadNumber'',						 N''v'','+cast (@ProductSaleProfileId as varchar(3))+', 0, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
			INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (4, 2, 0,0, N''[lm].[LeadEmail]'', NULL, N''LeadEmail'',							 N''a'','+cast (@ProductSaleProfileId as varchar(3))+', 0, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
			INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (5, 2, 1,0, N''[lm].[CreatedDate]'', NULL, N''CreatedDate'',					     N''a'','+cast (@ProductSaleProfileId as varchar(3))+', 4, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
			INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (6, 2, 1,0, N''[lm].[CreatedDateUTC]'', NULL, N''CreatedDateUTC'',				 N''v'','+cast (@ProductSaleProfileId as varchar(3))+', 4, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
			INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (7, 2, 1,0, N''[lm].[LastModifiedDate]'', NULL, N''LastModifiedDate'',			 N''a'','+cast (@ProductSaleProfileId as varchar(3))+', 4, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
			INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (8, 2, 1,0, N''[lm].[LastModifiedDateUTC]'', NULL, N''LastModifiedDateUTC'',	     N''v'','+cast (@ProductSaleProfileId as varchar(3))+', 4, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
			INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (9, 2, 1,0, N''[lm].[CreatedByLID]'', N''CreatedByLID/'', N''LID'',				 N''v'','+cast (@ProductSaleProfileId as varchar(3))+', 9, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
			INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (10, 2, 0,0, N''(SELECT   fullname  FROM dbo.employee where employeeid=[lm].[CreatedByLID]  ) '', N''CreatedByLID/'', N''Name'',				 N''v'', '+cast (@ProductSaleProfileId as varchar(3))+', 0, NULL, 9, N''LeadSearch'', 0)
			INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (11, 2, 1,0, N''[lm].[LastModifiedByLID]'', N''LastModifiedByLID/'', N''LID'',																 N''v'', '+cast (@ProductSaleProfileId as varchar(3))+', 9, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
			INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (12, 2, 1,0, N''(SELECT   fullname  FROM dbo.employee where employeeid=[lm].[LastModifiedByLID]  ) '', N''LastModifiedByLID/'', N''Name'',    N''v'', '+cast (@ProductSaleProfileId as varchar(3))+', 0, NULL, 11, N''LeadSearch'', 0)
			INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (13, 2, 0,1, N''[lm].[AsignToLID]'', N''AsignToLID/'', N''LID'',																			     N''v'', '+cast (@ProductSaleProfileId as varchar(3))+', 9, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
			INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (14, 2, 0,1, N'' (SELECT   fullname  FROM dbo.employee where employeeid=[lm].[AsignToLID]  ) '', N''AsignToLID/'', N''Name'',				 N''v'', '+cast (@ProductSaleProfileId as varchar(3))+', 0, NULL, 13, N''LeadSearch'', 0)
			INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (15, 2, 0,1, N''[lm].[LeadSourceId]'', N''LeadSourceId/'', N''Id'',																			 N''v'', '+cast (@ProductSaleProfileId as varchar(3))+', 6, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
			INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (16, 2, 0,0, N''(SELECT   [LeadSourceId],LeadSource   FROM cust.['+cast (@ProductSaleProfileId as varchar(3))+'_LeadSource]   FOR JSON AUTO) '', N''LeadSourceId/'', N''Data'',				 N''v'', '+cast (@ProductSaleProfileId as varchar(3))+', 0, NULL, 15, N''LeadSearch'', 0)
			INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (17, 2, 0,0, N''(SELECT   LeadSource  FROM cust.['+cast (@ProductSaleProfileId as varchar(3))+'_LeadSource] where LeadSourceId=[lm].[LeadSourceId]  )'', N''LeadSourceId/'', N''Name'',		 N''v'', '+cast (@ProductSaleProfileId as varchar(3))+', 0, NULL, 15, N''LeadSearch'', 0)
			INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (18, 2, 0,0, N''[lm].[LeadStatusId]'', N''LeadStatusId/'', N''Id'',																															 N''v'', '+cast (@ProductSaleProfileId as varchar(3))+', 6, N''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadMaster]'', 0, N''LeadSearch'', 0)
			INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (19, 2, 0,0, N''(SELECT   [LeadStatusId],LeadStatus   FROM cust.['+cast (@ProductSaleProfileId as varchar(3))+'_LeadStatus]   FOR JSON AUTO) '', N''LeadStatusId/'', N''Data'',				 N''v'', '+cast (@ProductSaleProfileId as varchar(3))+', 0, NULL, 18, N''LeadSearch'', 0)
			INSERT [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_CustTableInfo] ( [ColumnId],[TableId],[IsReadOnly], [IsCompulsory], [ColumnName], [ParentName], [ASCaption], [IsVisible],  [ProductSaleProfileId], [LookColumnTypeId], [TableName], [ParentLID], [QueryType], [EmployeeId]) VALUES (20, 2, 0,0, N''(SELECT   LeadStatus  FROM cust.['+cast (@ProductSaleProfileId as varchar(3))+'_LeadStatus] where LeadStatusId=[lm].[LeadStatusId]  )'', N''LeadStatusId/'', N''Name'',		 N''v'', '+cast (@ProductSaleProfileId as varchar(3))+', 0, NULL, 18, N''LeadSearch'', 0)

'
--print (@vari)	
exec(@vari)	





/* column info*/
set @vari='INSERT INTO [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_TableColumnInfo] ([ColumnName] ,[TableName] ,[ColumnCatId] ,[ColumnType] ,[CreateDate] ,[CreatedBy] ,[ColumnStatudId]  ,[UpdatedDate]  ,[UpdatedBy] ,[ParentId] ,[ForginKeyTableName])
     VALUES     (''LeadStatusId''  ,''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadStatus]''  ,1   ,''[int] IDENTITY''  ,getdate()  ,0  ,1  ,NULL   ,0  0 ,NULL)';
exec(@vari)
set @vari='INSERT INTO [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_TableColumnInfo] ([ColumnName] ,[TableName] ,[ColumnCatId] ,[ColumnType] ,[CreateDate] ,[CreatedBy] ,[ColumnStatudId]  ,[UpdatedDate]  ,[UpdatedBy] ,[ParentId] ,[ForginKeyTableName])
     VALUES     (''LeadStatus''  ,''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadStatus]''  ,1   ,''varchar(200)''  ,getdate()  ,0  ,1  ,NULL   ,0  0 ,NULL)';
exec(@vari)
  set @vari='INSERT INTO [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_TableColumnInfo] ([ColumnName] ,[TableName]                                                              ,[ColumnCatId] ,[ColumnType]   ,[CreateDate] ,[CreatedBy] ,[ColumnStatudId]  ,[UpdatedDate]  ,[UpdatedBy] ,[ParentId] ,[ForginKeyTableName])
																					 VALUES     (''LeadSourceId''  ,''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadSource]''  ,1          ,''[int] IDENTITY''  ,getdate()   ,0                ,1                 ,NULL          ,0            0                 ,NULL)';
exec(@vari)

  set @vari='INSERT INTO [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_TableColumnInfo] ([ColumnName] ,[TableName]                                                              ,[ColumnCatId] ,[ColumnType]   ,[CreateDate] ,[CreatedBy] ,[ColumnStatudId]  ,[UpdatedDate]  ,[UpdatedBy] ,[ParentId] ,[ForginKeyTableName])
																					 VALUES     (''LeadSource''  ,''[CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_LeadSource]''  ,1          ,''varchar(200)''  ,getdate()   ,0                ,1                 ,NULL          ,0            0                 ,NULL)';
exec(@vari)




 set @vari='CREATE TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_Employee](
	[EmployeeId] [bigint] IDENTITY(1,1) NOT NULL,
	[FullName] [varchar](250) NOT NULL,
	[FirstName] [varchar](250) NOT NULL,
	[PasswordHash] [varchar](250) NULL,
	[EmployeeGuid] [uniqueidentifier] NULL,
	[EmployeeNum] [varchar](40) NULL,
	[LastName] [varchar](250) NULL,
	[Email] [varchar](250) NULL,
	[FatherName] [varchar](250) NULL,
	[HusbandName] [varchar](250) NULL,
	[Gender] [char](10) NULL,
	[MaritalStatus] [bigint] NULL,
	[CNIC] [varchar](50) NULL,
	[IsDisable] [bit] NOT NULL,
	[DisableDetail] [varchar](250) NULL,
	[PresentAddress] [varchar](500) NULL,
	[PermanentAddress] [varchar](500) NULL,
	[MobileNo] [varchar](250) NULL,
	[ConatctInfo] [varchar](250) NULL,
	[ICEContactInfo] [varchar](250) NULL,
	[DateOfBirth] [date] NULL,
	[Starttime] [time](7) NULL,
	[OffTime] [time](7) NULL,
	[ShortSummary] [varchar](500) NULL,
	[HRRemarks] [varchar](500) NULL,
	[NoYearsOfExperience] [bigint] NULL,
	[StartSalary] [float] NULL,
	[ReferenceName] [varchar](250) NULL,
	[ReferenceEmail] [varchar](250) NULL,
	[DateOfJoining] [datetime] NULL,
	[ManagerId] [bigint] NULL,
	[LookEmployeeTypeId] [bigint] NULL,
	[LookDepartmentId] [bigint] NULL,
	[LookDesignationId] [bigint] NULL,
	[ExitDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[ApplicantId] [bigint] NULL,
	[ShiftSettingId] [bigint] NULL,
	[LookGradeId] [bigint] NULL,
	[BioMetricId] [int] NULL,
	[LookOrganizationId] [bigint] NULL,
	[RoleId] [bigint] NULL,
	[LookOrganizationIds] [varchar](100) NULL,
	[IsActive] [bit] NULL,
	[SubLeadIds] [varchar](100) NULL,
	[HasSubOrdinates] [bit] NULL,
	[WorkingHourPolicyId] [int] NULL,
	[IsUserOnly] [bit] NOT NULL,
	[ProductSaleProfileId] [int] NOT NULL,
	[BioMetricInfoId] [bigint] NULL,
 CONSTRAINT [PK_Employee_'+cast (@ProductSaleProfileId as varchar(3))+'] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_Employee] ADD  CONSTRAINT [DF_Employee_'+cast (@ProductSaleProfileId as varchar(3))+'_IsDisable]  DEFAULT ((0)) FOR [IsDisable]
ALTER TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_Employee] ADD  CONSTRAINT [DF_Employee_'+cast (@ProductSaleProfileId as varchar(3))+'_IsUserOnly]  DEFAULT ((0)) FOR [IsUserOnly]
ALTER TABLE [CUST].['+cast (@ProductSaleProfileId as varchar(3))+'_Employee] ADD  CONSTRAINT [DF_Employee_'+cast (@ProductSaleProfileId as varchar(3))+'_ProductSaleProfileId]  DEFAULT ((0)) FOR [ProductSaleProfileId]';

-- exec(@vari)	
 END TRY
  BEGIN CATCH
    INSERT INTO dbo.DB_Errors
    VALUES
  (SUSER_SNAME(),
   ERROR_NUMBER(),
   ERROR_STATE(),
   ERROR_SEVERITY(),
   ERROR_LINE(),
   ERROR_PROCEDURE(),
   ERROR_MESSAGE(),
   GETDATE(),@ProductSaleProfileId,@vari);
  END CATCH

END
