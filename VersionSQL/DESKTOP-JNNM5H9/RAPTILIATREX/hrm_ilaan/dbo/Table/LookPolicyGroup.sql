﻿/****** Object:  Table [dbo].[LookPolicyGroup]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[LookPolicyGroup](
	[LookPolicyGroupId] [bigint] IDENTITY(1,1) NOT NULL,
	[PolicyGroupName] [nvarchar](50) NOT NULL,
	[GroupTypeId] [bigint] NULL,
 CONSTRAINT [PK_LookPolicyGroup] PRIMARY KEY CLUSTERED 
(
	[LookPolicyGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
