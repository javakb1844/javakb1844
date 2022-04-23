/****** Object:  Table [dbo].[EmployeeEducation]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[EmployeeEducation](
	[EmployeeEducationId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[LookQualificationId] [bigint] NULL,
	[EducationYear] [nvarchar](50) NULL,
	[LookInstitutionId] [bigint] NULL,
	[Grade] [nvarchar](50) NULL,
 CONSTRAINT [PK_EmployeeEducation] PRIMARY KEY CLUSTERED 
(
	[EmployeeEducationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
