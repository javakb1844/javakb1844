/****** Object:  Table [dbo].[Employee]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[Employee](
	[EmployeeId] [bigint] IDENTITY(1,1) NOT NULL,
	[FullName] [varchar](250) NOT NULL,
	[FirstName] [varchar](250) NOT NULL,
	[PasswordHash] [varchar](250) NULL,
	[EmployeeGuid] [uniqueidentifier] NULL,
	[EmployeeNum] [varchar](40) NULL,
	[LastName] [varchar](250) NULL,
	[Email] [varchar](250) NULL,
	[FatherName] [varchar](250) NULL,
	[HusbandName] [varchar](250) NULL,
	[Gender] [char](10) NULL,
	[MaritalStatus] [bigint] NULL,
	[CNIC] [varchar](50) NULL,
	[IsDisable] [bit] NOT NULL,
	[DisableDetail] [varchar](250) NULL,
	[PresentAddress] [varchar](500) NULL,
	[PermanentAddress] [varchar](500) NULL,
	[MobileNo] [varchar](250) NULL,
	[ConatctInfo] [varchar](250) NULL,
	[ICEContactInfo] [varchar](250) NULL,
	[DateOfBirth] [date] NULL,
	[Starttime] [time](7) NULL,
	[OffTime] [time](7) NULL,
	[ShortSummary] [varchar](500) NULL,
	[HRRemarks] [varchar](500) NULL,
	[NoYearsOfExperience] [bigint] NULL,
	[StartSalary] [float] NULL,
	[ReferenceName] [varchar](250) NULL,
	[ReferenceEmail] [varchar](250) NULL,
	[DateOfJoining] [datetime] NULL,
	[ManagerId] [bigint] NULL,
	[LookEmployeeTypeId] [bigint] NULL,
	[LookDepartmentId] [bigint] NULL,
	[LookDesignationId] [bigint] NULL,
	[ExitDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[ApplicantId] [bigint] NULL,
	[ShiftSettingId] [bigint] NULL,
	[LookGradeId] [bigint] NULL,
	[BioMetricId] [int] NULL,
	[LookOrganizationId] [bigint] NULL,
	[RoleId] [bigint] NULL,
	[LookOrganizationIds] [varchar](100) NULL,
	[IsActive] [bit] NULL,
	[SubLeadIds] [varchar](100) NULL,
	[HasSubOrdinates] [bit] NULL,
	[WorkingHourPolicyId] [int] NULL,
	[IsUserOnly] [bit] NOT NULL,
	[ProductSaleProfileId] [int] NOT NULL,
	[BioMetricInfoId] [bigint] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [DF_Employee_IsDisable]  DEFAULT ((0)) FOR [IsDisable]
ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [DF_Employee_IsUserOnly]  DEFAULT ((0)) FOR [IsUserOnly]
ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [DF_Employee_ProductSaleProfileId]  DEFAULT ((0)) FOR [ProductSaleProfileId]
