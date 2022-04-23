/****** Object:  Table [dbo].[LookGender]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[LookGender](
	[LookGenderId] [bigint] NOT NULL,
	[LookGenderName] [nvarchar](300) NULL,
 CONSTRAINT [PK_LookGender] PRIMARY KEY CLUSTERED 
(
	[LookGenderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
