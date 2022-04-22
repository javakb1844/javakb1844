/****** Object:  Table [dbo].[EmployeePolicy]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[EmployeePolicy](
	[EmployeePolicyId] [bigint] IDENTITY(1,1) NOT NULL,
	[LookPolicyId] [bigint] NULL,
	[PolicyName] [nvarchar](150) NULL,
	[PolicyUnit] [nvarchar](50) NULL,
	[LookPolicyGroupId] [bigint] NULL,
	[PolicyValue] [nvarchar](100) NULL,
	[EmployeeId] [bigint] NOT NULL,
 CONSTRAINT [PK_EmployeePolicy] PRIMARY KEY CLUSTERED 
(
	[EmployeePolicyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
