/****** Object:  Table [dbo].[LookDesignation]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[LookDesignation](
	[LookDesignationId] [bigint] NOT NULL,
	[DesignationName] [nvarchar](150) NOT NULL,
	[LookDepartmentId] [bigint] NULL,
	[ProductSaleProfileId] [int] NULL,
 CONSTRAINT [PK_LookDesignation] PRIMARY KEY CLUSTERED 
(
	[LookDesignationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
