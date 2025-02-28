USE [Test_cathaybk]
GO

/****** Object:  StoredProcedure [api_Front].[usp_APIRequest_Create]    Script Date: 2025/2/28 ¤U¤È 02:30:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [api_Front].[usp_APIRequest_Create] 
(
	@APIPath varchar(50)
	, @RequestBody nvarchar(2000) = ''
	, @UserHostAddress varchar(30) = ''
	, @UserAgent varchar(100) = ''
	, @UrlReferrer varchar(500) = ''
)
AS
BEGIN
	SET NOCOUNT ON;

	IF object_id('tempdb..#TempResult') IS NOT NULL
		drop table #TempResult

	declare @SPName nvarchar(100) = ''

	SELECT @SPName = OBJECT_NAME(@@PROCID)

	BEGIN TRY
		create table #TempResult(IsSuccess bit Default(0), Msg nvarchar(500) Default(''), ID int)

		insert into logs.APIRequest(APIPath, RequestBody, UserHostAddress, UserAgent, UrlReferrer, CreateSPName)
		output 1, '', inserted.ID into #TempResult(IsSuccess, Msg, ID)
		VALUES(@APIPath, @RequestBody, @UserHostAddress, @UserAgent, @UrlReferrer, @SPName)

	END TRY
	BEGIN CATCH

		declare @ErrorMessage nvarchar(500) = ''
		set @ErrorMessage = ERROR_MESSAGE()

		insert into #TempResult(IsSuccess, Msg)
		VALUES(0, @ErrorMessage)

	END CATCH

	select IsSuccess, Msg, ID from #TempResult

END
GO


