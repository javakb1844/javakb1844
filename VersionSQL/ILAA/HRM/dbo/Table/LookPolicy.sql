/****** Object:  Table [dbo].[LookPolicy]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[LookPolicy](
	[LookPolicyId] [bigint] IDENTITY(1,1) NOT NULL,
	[PolicyName] [varchar](250) NULL,
	[PolicyUnit] [varchar](100) NULL,
	[LookPolicyGroupId] [int] NOT NULL,
	[ValueType] [int] NULL,
	[IsPercentage] [bit] NULL,
 CONSTRAINT [PK_LookPolicy] PRIMARY KEY CLUSTERED 
(
	[LookPolicyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[LookPolicy] ADD  CONSTRAINT [DF_LookPolicy_LookPolicyGroupId]  DEFAULT ((1)) FOR [LookPolicyGroupId]
ALTER TABLE [dbo].[LookPolicy] ADD  CONSTRAINT [DF_LookPolicy_ValueType]  DEFAULT ((0)) FOR [ValueType]
