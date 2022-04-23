/****** Object:  Table [dbo].[Users]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[Users](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](250) NULL,
	[EmployeeId] [bigint] NULL,
	[PasswordHash] [nvarchar](250) NULL,
	[CreateDate] [datetime] NULL,
	[Updatedate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[VerificationKey] [nvarchar](max) NULL,
	[IsLockedOut] [bit] NULL,
	[LastLoginDate] [datetime] NULL,
	[LastPasswordChangedDate] [datetime] NULL,
	[LastLockoutDate] [datetime] NULL,
	[FailedPasswordAttemptCount] [bigint] NULL,
	[UserGuid] [uniqueidentifier] NULL,
	[FirstName] [nvarchar](250) NULL,
	[LastName] [nvarchar](250) NULL,
	[Token] [varchar](250) NULL,
	[RoleId] [bigint] NOT NULL,
	[Coments] [ntext] NULL,
	[MobileNumber] [varchar](250) NULL,
	[PhoneNumber] [varchar](250) NULL,
	[Address] [varchar](300) NULL,
	[Postcode] [varchar](50) NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedBy] [bigint] NULL,
	[TempPassword] [nvarchar](250) NULL,
	[TempPasswordDate] [datetime] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
