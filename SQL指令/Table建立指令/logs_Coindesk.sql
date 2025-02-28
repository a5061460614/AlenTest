USE [Test_cathaybk]
GO

/****** Object:  Table [logs].[Coindesk]    Script Date: 2025/2/28 ¤U¤È 02:30:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [logs].[Coindesk](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Coindesk_ID] [uniqueidentifier] NOT NULL,
	[CoinName_Eng] [varchar](20) NOT NULL,
	[CoinName_Cht] [nvarchar](50) NOT NULL,
	[CreateSPName] [varchar](100) NOT NULL,
	[CreateDatetime] [datetime] NOT NULL,
	[UpdateSPName] [varchar](100) NOT NULL,
	[UpdateDatetime] [datetime] NOT NULL,
	[ActionType] [varchar](5) NOT NULL,
	[ActionDatetime] [datetime] NOT NULL,
	[ActionSPName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Test_cathaybk] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [logs].[Coindesk] ADD  CONSTRAINT [DF_Test_cathaybk_ID]  DEFAULT (newid()) FOR [Coindesk_ID]
GO

ALTER TABLE [logs].[Coindesk] ADD  CONSTRAINT [DF_Test_cathaybk_CoinName_Eng]  DEFAULT ('') FOR [CoinName_Eng]
GO

ALTER TABLE [logs].[Coindesk] ADD  CONSTRAINT [DF_Test_cathaybk_CoinName_Cht]  DEFAULT ('') FOR [CoinName_Cht]
GO

ALTER TABLE [logs].[Coindesk] ADD  CONSTRAINT [DF_Test_cathaybk_CreateSPName]  DEFAULT ('') FOR [CreateSPName]
GO

ALTER TABLE [logs].[Coindesk] ADD  CONSTRAINT [DF_Test_cathaybk_CreateDatetime]  DEFAULT (getdate()) FOR [CreateDatetime]
GO

ALTER TABLE [logs].[Coindesk] ADD  CONSTRAINT [DF_Test_cathaybk_UpdateSPName]  DEFAULT ('') FOR [UpdateSPName]
GO

ALTER TABLE [logs].[Coindesk] ADD  CONSTRAINT [DF_Test_cathaybk_UpdateDatetime]  DEFAULT (getdate()) FOR [UpdateDatetime]
GO

ALTER TABLE [logs].[Coindesk] ADD  CONSTRAINT [DF_Test_cathaybk_ActionType]  DEFAULT ('') FOR [ActionType]
GO

ALTER TABLE [logs].[Coindesk] ADD  CONSTRAINT [DF_Test_cathaybk_ActionDatetime]  DEFAULT (getdate()) FOR [ActionDatetime]
GO

ALTER TABLE [logs].[Coindesk] ADD  CONSTRAINT [DF_Test_cathaybk_ActionSPName]  DEFAULT ('') FOR [ActionSPName]
GO


