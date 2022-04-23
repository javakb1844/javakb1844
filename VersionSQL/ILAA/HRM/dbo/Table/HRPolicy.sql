/****** Object:  Table [dbo].[HRPolicy]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[HRPolicy](
	[HRPolicyId] [bigint] IDENTITY(1,1) NOT NULL,
	[LookDesignationId] [bigint] NOT NULL,
	[LookPolicyId] [bigint] NOT NULL,
	[PolicyValue] [nvarchar](50) NOT NULL,
	[GroupTypeId] [bigint] NOT NULL,
	[ProductSaleProfileId] [int] NULL,
 CONSTRAINT [PK_HRPolicy] PRIMARY KEY CLUSTERED 
(
	[HRPolicyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[HRPolicy] ADD  CONSTRAINT [DF_HRPolicy_GroupTypeId]  DEFAULT ((1)) FOR [GroupTypeId]
