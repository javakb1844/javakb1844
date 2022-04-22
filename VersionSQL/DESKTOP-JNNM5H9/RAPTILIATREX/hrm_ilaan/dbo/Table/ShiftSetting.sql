/****** Object:  Table [dbo].[ShiftSetting]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[ShiftSetting](
	[ShiftSettingId] [bigint] IDENTITY(1,1) NOT NULL,
	[ShiftName] [nvarchar](150) NOT NULL,
	[StartTime] [time](7) NULL,
	[EndTime] [time](7) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_ShiftSetting] PRIMARY KEY CLUSTERED 
(
	[ShiftSettingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[ShiftSetting] ADD  CONSTRAINT [DF_ShiftSetting_IsActive]  DEFAULT ((0)) FOR [IsActive]
