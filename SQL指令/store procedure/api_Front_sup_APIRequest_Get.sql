USE [Test_cathaybk]
GO

/****** Object:  StoredProcedure [api_Front].[usp_APIRequest_Get]    Script Date: 2025/2/28 ¤U¤È 02:31:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [api_Front].[usp_APIRequest_Get] 
AS
BEGIN
	SET NOCOUNT ON;

	IF object_id('tempdb..#TempResult') IS NOT NULL
		drop table #TempResult

	declare @SPName nvarchar(100) = ''

	SELECT @SPName = OBJECT_NAME(@@PROCID)

	BEGIN TRY
		create table #TempResult(IsSuccess bit Default(0), Msg nvarchar(500) Default(''), ID int, APIPath varchar(100) Default(''), RequestBody nvarchar(2000), ResponseString nvarchar(2000), ResponseSuccess bit Default(0)
			,UserHostAddress varchar(30) Default(''), UserAgent varchar(100) Default(''), UrlReferrer varchar(500) Default(''), CreateDatetime varchar(30) Default(''))

		insert into #TempResult(IsSuccess, Msg, ID, APIPath, RequestBody, ResponseString, ResponseSuccess, UserHostAddress, UserAgent, UrlReferrer, CreateDatetime)
		select 1, '', ID, APIPath, RequestBody, ResponseString, ResponseSuccess, UserHostAddress, UserAgent, UrlReferrer, Convert(varchar(20), CreateDatetime, 120) CreateDatetime 
		from logs.APIRequest
		order by CreateDatetime desc

	END TRY
	BEGIN CATCH

		declare @ErrorMessage nvarchar(500) = ''
		set @ErrorMessage = ERROR_MESSAGE()

		insert into #TempResult(IsSuccess, Msg)
		VALUES(0, @ErrorMessage)

	END CATCH

	select IsSuccess, Msg, ID, APIPath, RequestBody, ResponseString, ResponseSuccess, UserHostAddress, UserAgent, UrlReferrer
	from #TempResult

END
GO


