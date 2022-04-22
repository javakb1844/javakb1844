/****** Object:  Table [dbo].[LookCountry]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[LookCountry](
	[LookCountryId] [bigint] IDENTITY(1,1) NOT NULL,
	[CountryName] [nvarchar](250) NULL,
 CONSTRAINT [PK_LookCountry] PRIMARY KEY CLUSTERED 
(
	[LookCountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
