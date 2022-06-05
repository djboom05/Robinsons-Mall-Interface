USE [POS]
GO

/****** Object:  Table [dbo].[mallinterface_ngt]    Script Date: 03/01/2022 12:39:49 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[mallinterface_ngt](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[businessdate] [date] NULL,
	[ngt] [nvarchar](50) NOT NULL,
	[date_sent] [datetime] NULL
) ON [PRIMARY]

GO

