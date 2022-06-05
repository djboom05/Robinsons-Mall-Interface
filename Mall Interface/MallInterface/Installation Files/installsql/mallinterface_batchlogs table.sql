USE [POS]
GO

/****** Object:  Table [dbo].[mallinterface_batchlogs]    Script Date: 03/01/2022 12:39:49 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[mallinterface_batchlogs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[filename] [nvarchar](50) NULL,
	[businessdate] [date] NULL,
	[date_sent] [datetime] NULL
) ON [PRIMARY]

GO

