/****** Object:  Table [dbo].[PublicHoliday]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[PublicHoliday](
	[PublicHolidayId] [bigint] IDENTITY(1,1) NOT NULL,
	[HoIidayDate] [date] NOT NULL,
	[Detail] [nvarchar](200) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_PublicHoliday] PRIMARY KEY CLUSTERED 
(
	[PublicHolidayId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[PublicHoliday] ADD  CONSTRAINT [DF_PublicHoliday_IsActive]  DEFAULT ((0)) FOR [IsActive]
