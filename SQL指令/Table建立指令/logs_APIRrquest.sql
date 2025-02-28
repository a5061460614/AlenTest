USE [Test_cathaybk]
GO

/****** Object:  Table [logs].[APIRequest]    Script Date: 2025/2/28 ¤U¤È 02:29:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [logs].[APIRequest](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[APIPath] [varchar](100) NOT NULL,
	[RequestBody] [nvarchar](2000) NOT NULL,
	[ResponseString] [nvarchar](2000) NOT NULL,
	[ResponseSuccess] [bit] NOT NULL,
	[UserHostAddress] [varchar](30) NOT NULL,
	[UserAgent] [varchar](100) NOT NULL,
	[UrlReferrer] [varchar](500) NOT NULL,
	[CreateDatetime] [datetime] NOT NULL,
	[CreateSPName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_APIRequest] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [logs].[APIRequest] ADD  CONSTRAINT [DF_APIRequest_APIName]  DEFAULT ('') FOR [APIPath]
GO

ALTER TABLE [logs].[APIRequest] ADD  CONSTRAINT [DF_APIRequest_RequestString]  DEFAULT ('') FOR [RequestBody]
GO

ALTER TABLE [logs].[APIRequest] ADD  CONSTRAINT [DF_APIRequest_ResponseString]  DEFAULT ('') FOR [ResponseString]
GO

ALTER TABLE [logs].[APIRequest] ADD  CONSTRAINT [DF_APIRequest_ResponseSuccess]  DEFAULT ((0)) FOR [ResponseSuccess]
GO

ALTER TABLE [logs].[APIRequest] ADD  CONSTRAINT [DF_APIRequest_UserHostAddress]  DEFAULT ('') FOR [UserHostAddress]
GO

ALTER TABLE [logs].[APIRequest] ADD  CONSTRAINT [DF_APIRequest_UserAgent]  DEFAULT ('') FOR [UserAgent]
GO

ALTER TABLE [logs].[APIRequest] ADD  CONSTRAINT [DF_APIRequest_UrlReferrer]  DEFAULT ('') FOR [UrlReferrer]
GO

ALTER TABLE [logs].[APIRequest] ADD  CONSTRAINT [DF_APIRequest_CreateDatetime]  DEFAULT (getdate()) FOR [CreateDatetime]
GO

ALTER TABLE [logs].[APIRequest] ADD  CONSTRAINT [DF_APIRequest_CreateSPName]  DEFAULT ('') FOR [CreateSPName]
GO


