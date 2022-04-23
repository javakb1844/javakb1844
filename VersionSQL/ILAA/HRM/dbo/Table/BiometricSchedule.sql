/****** Object:  Table [dbo].[BiometricSchedule]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[BiometricSchedule](
	[BiometricScheduleId] [int] IDENTITY(1,1) NOT NULL,
	[TimeSchedule] [time](7) NOT NULL,
	[IsActive] [bit] NULL,
	[ProductSaleProfileId] [int] NULL,
 CONSTRAINT [PK_BiometricSchedule] PRIMARY KEY CLUSTERED 
(
	[BiometricScheduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[BiometricSchedule] ADD  CONSTRAINT [DF_BiometricSchedule_IsActive]  DEFAULT ((1)) FOR [IsActive]
