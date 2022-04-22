/****** Object:  Table [dbo].[EmployeeLog]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[EmployeeLog](
	[EmployeeLogId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[FullName] [nvarchar](250) NOT NULL,
	[FirstName] [nvarchar](250) NOT NULL,
	[LastName] [nvarchar](250) NULL,
	[Email] [nvarchar](250) NULL,
	[FatherName] [nvarchar](250) NULL,
	[HusbandName] [nvarchar](250) NULL,
	[Gender] [char](10) NULL,
	[MaritalStatus] [bigint] NULL,
	[CNIC] [nvarchar](15) NULL,
	[IsDisable] [bit] NOT NULL,
	[DisableDetail] [nvarchar](250) NULL,
	[PresentAddress] [nvarchar](500) NULL,
	[PermanentAddress] [nvarchar](500) NULL,
	[MobileNo] [nvarchar](250) NULL,
	[ConatctInfo] [nvarchar](250) NULL,
	[ICEContactInfo] [nvarchar](250) NULL,
	[DateOfBirth] [date] NULL,
	[ShortSummary] [nvarchar](500) NULL,
	[HRRemarks] [nvarchar](500) NULL,
	[NoYearsOfExperience] [bigint] NULL,
	[StartSalary] [float] NULL,
	[ReferenceName] [nvarchar](250) NULL,
	[ReferenceEmail] [nvarchar](250) NULL,
	[DateOfJoining] [datetime] NULL,
	[ManagerId] [bigint] NULL,
	[LookEmployeeTypeId] [bigint] NULL,
	[LookDepartmentId] [bigint] NULL,
	[LookDesignationId] [bigint] NULL,
	[ExitDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [datetime] NULL,
	[LogCreateDate] [datetime] NULL,
	[LogCeatedBy] [bigint] NULL,
 CONSTRAINT [PK_EmployeeLog] PRIMARY KEY CLUSTERED 
(
	[EmployeeLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
