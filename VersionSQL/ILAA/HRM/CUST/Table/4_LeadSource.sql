﻿/****** Object:  Table [CUST].[4_LeadSource]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [CUST].[4_LeadSource](
	[LeadSourceId] [int] IDENTITY(1,1) NOT NULL,
	[LeadSource] [varchar](200) NOT NULL,
 CONSTRAINT [PK_LeadSource_4] PRIMARY KEY CLUSTERED 
(
	[LeadSourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
