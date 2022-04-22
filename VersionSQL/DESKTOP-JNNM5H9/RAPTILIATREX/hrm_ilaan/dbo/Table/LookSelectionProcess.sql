/****** Object:  Table [dbo].[LookSelectionProcess]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[LookSelectionProcess](
	[LookSelectionProcessId] [bigint] IDENTITY(1,1) NOT NULL,
	[SelectionProcess] [nvarchar](450) NULL,
	[Type] [bigint] NULL,
 CONSTRAINT [PK_LookSelectionProcess] PRIMARY KEY CLUSTERED 
(
	[LookSelectionProcessId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[LookSelectionProcess] ADD  CONSTRAINT [DF_LookSelectionProcess_Type]  DEFAULT ((0)) FOR [Type]
