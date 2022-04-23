/****** Object:  Table [dbo].[Recruitment]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[Recruitment](
	[RecruitmentId] [bigint] IDENTITY(1,1) NOT NULL,
	[LookDesignationId] [bigint] NULL,
	[LookBranchId] [bigint] NULL,
	[LookEmployeeTypeId] [bigint] NULL,
	[Preferred_Start_Date] [datetime] NULL,
	[AdClosingDate] [datetime] NULL,
	[LookJobTypeId] [bigint] NULL,
	[LookDepartmentId] [bigint] NULL,
	[ReportingEmployeeId] [bigint] NULL,
	[Length_Of_Term] [bigint] NULL,
	[LookRecruitmentStatusId] [bigint] NOT NULL,
	[NoOfPositions] [bigint] NULL,
	[LookGenderId] [bigint] NULL,
	[PositionDescription] [nvarchar](450) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[ForApprovalEmployeeId] [bigint] NULL,
	[ShortSummary] [nvarchar](1500) NULL,
 CONSTRAINT [PK_Recruitment] PRIMARY KEY CLUSTERED 
(
	[RecruitmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
