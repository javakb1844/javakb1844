﻿/****** Object:  Table [dbo].[LookEmployeeType]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[LookEmployeeType](
	[LookEmployeeTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeTypeName] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_LookEmployeeType] PRIMARY KEY CLUSTERED 
(
	[LookEmployeeTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
