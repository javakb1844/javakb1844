/****** Object:  Table [dbo].[HRPolicyByCaption]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[HRPolicyByCaption](
	[HRPolicyByCaptionId] [bigint] IDENTITY(1,1) NOT NULL,
	[HRPolicyCaptionId] [int] NOT NULL,
	[HRPolicyCaption] [varchar](250) NULL,
	[LookPolicyId] [bigint] NOT NULL,
	[PolicyValue] [varchar](50) NOT NULL,
	[GroupTypeId] [bigint] NOT NULL,
	[CreateDate] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[PolicyName] [varchar](250) NULL,
	[LookPolicyGroupId] [bigint] NULL,
 CONSTRAINT [PK_HRPolicyByCaption] PRIMARY KEY CLUSTERED 
(
	[HRPolicyByCaptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[HRPolicyByCaption] ADD  CONSTRAINT [DF_HRPolicyByCaption_GroupTypeId]  DEFAULT ((1)) FOR [GroupTypeId]
