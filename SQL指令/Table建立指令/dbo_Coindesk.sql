USE [Test_cathaybk]
GO

/****** Object:  Table [dbo].[Coindesk]    Script Date: 2025/2/28 ¤U¤È 02:29:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Coindesk](
	[ID] [uniqueidentifier] NOT NULL,
	[CoinName_Eng] [varchar](20) NOT NULL,
	[CoinName_Cht] [nvarchar](50) NOT NULL,
	[CreateSPName] [varchar](100) NOT NULL,
	[CreateDatetime] [datetime] NOT NULL,
	[UpdateSPName] [varchar](100) NOT NULL,
	[UpdateDatetime] [datetime] NOT NULL,
 CONSTRAINT [PK_Coindesk] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Coindesk] ADD  CONSTRAINT [DF_Coindesk_ID]  DEFAULT (newid()) FOR [ID]
GO

ALTER TABLE [dbo].[Coindesk] ADD  CONSTRAINT [DF_Coindesk_CoinName_Eng]  DEFAULT ('') FOR [CoinName_Eng]
GO

ALTER TABLE [dbo].[Coindesk] ADD  CONSTRAINT [DF_Coindesk_CoinName_Cht]  DEFAULT ('') FOR [CoinName_Cht]
GO

ALTER TABLE [dbo].[Coindesk] ADD  CONSTRAINT [DF_Coindesk_CreateSPName]  DEFAULT ('') FOR [CreateSPName]
GO

ALTER TABLE [dbo].[Coindesk] ADD  CONSTRAINT [DF_Coindesk_CreateDatetime]  DEFAULT (getdate()) FOR [CreateDatetime]
GO

ALTER TABLE [dbo].[Coindesk] ADD  CONSTRAINT [DF_Table_1_CreateSPName1]  DEFAULT ('') FOR [UpdateSPName]
GO

ALTER TABLE [dbo].[Coindesk] ADD  CONSTRAINT [DF_Table_1_CreateDatetime1]  DEFAULT (getdate()) FOR [UpdateDatetime]
GO


