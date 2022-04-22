/****** Object:  Table [dbo].[Applicant]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[Applicant](
	[ApplicantId] [bigint] IDENTITY(1,1) NOT NULL,
	[FullName] [varchar](250) NOT NULL,
	[FirstName] [varchar](250) NOT NULL,
	[LastName] [varchar](250) NULL,
	[Email] [varchar](250) NULL,
	[Gender] [char](10) NULL,
	[MaritalStatus] [bigint] NULL,
	[CNIC] [varchar](50) NULL,
	[PresentAddress] [varchar](500) NULL,
	[PermanentAddress] [varchar](500) NULL,
	[MobileNo] [varchar](250) NULL,
	[ConatctInfo] [varchar](250) NULL,
	[DateOfBirth] [date] NULL,
	[ShortSummary] [varchar](500) NULL,
	[CoveringLetter] [varchar](200) NULL,
	[Resume] [varchar](200) NULL,
	[NoYearsOfExperience] [bigint] NULL,
	[Salary] [float] NULL,
	[LookQualificationId] [bigint] NULL,
	[HRRemarks] [varchar](500) NULL,
	[ApplicantRemarks] [varchar](500) NULL,
	[LookOrganizationId] [bigint] NULL,
	[ReferenceName] [varchar](250) NULL,
	[ReferenceEmail] [varchar](250) NULL,
	[ExpectedDateOfJoining] [datetime] NULL,
	[LookEmployeeTypeId] [bigint] NULL,
	[LookDepartmentId] [bigint] NULL,
	[LookDesignationId] [bigint] NULL,
	[CreateDate] [datetime] NULL,
	[CreatedBy] [bigint] NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[LookApplicantStatusId] [bigint] NULL,
	[SelectionGrade] [bigint] NULL,
 CONSTRAINT [PK_Applicant] PRIMARY KEY CLUSTERED 
(
	[ApplicantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
