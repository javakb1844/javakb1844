/****** Object:  Table [dbo].[EmployeeExperienceHistory]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[EmployeeExperienceHistory](
	[EmployeeExperienceHistoryId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[LookOrganizationId] [bigint] NULL,
	[FromDate] [datetime] NULL,
	[ToDate] [datetime] NULL,
	[JobTitle] [nvarchar](250) NULL,
	[JobDescription] [nvarchar](300) NULL,
	[LookCityId] [bigint] NULL,
	[LookCountryId] [bigint] NULL,
	[Salary] [float] NULL,
 CONSTRAINT [PK_EmployeeExperience] PRIMARY KEY CLUSTERED 
(
	[EmployeeExperienceHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
