﻿/****** Object:  Table [dbo].[HRPolicyCaption]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[HRPolicyCaption](
	[HRPolicyCaptionId] [int] IDENTITY(1,1) NOT NULL,
	[PolicyCaption] [varchar](250) NULL,
	[CreateDate] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[IsVariableTiming] [bit] NULL,
	[ProductSaleProfileId] [int] NULL,
 CONSTRAINT [PK_HRPolicyCaption] PRIMARY KEY CLUSTERED 
(
	[HRPolicyCaptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[HRPolicyCaption] ADD  CONSTRAINT [DF_HRPolicyCaption_IsVariableTiming]  DEFAULT ((0)) FOR [IsVariableTiming]
