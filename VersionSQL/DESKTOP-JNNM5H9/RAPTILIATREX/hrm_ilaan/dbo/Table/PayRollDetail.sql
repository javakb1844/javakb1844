/****** Object:  Table [dbo].[PayRollDetail]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[PayRollDetail](
	[PayRollDetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[PayRollId] [bigint] NOT NULL,
	[EmployeePolicyId] [bigint] NOT NULL,
	[LookPolicyId] [bigint] NOT NULL,
	[PolicyName] [nvarchar](250) NULL,
	[PolicyUnit] [nvarchar](50) NULL,
	[LookPolicyGroupId] [bigint] NULL,
	[PolicyValue] [decimal](18, 2) NULL,
	[ValueType] [bigint] NULL,
	[IsPercentage] [bit] NULL,
 CONSTRAINT [PK_PayRollDetail] PRIMARY KEY CLUSTERED 
(
	[PayRollDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
