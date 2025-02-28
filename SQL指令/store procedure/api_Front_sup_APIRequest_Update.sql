USE [Test_cathaybk]
GO

/****** Object:  StoredProcedure [api_Front].[usp_APIRequest_Update]    Script Date: 2025/2/28 下午 02:31:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [api_Front].[usp_APIRequest_Update] 
(
	@ID int
	, @ResponseString nvarchar(2000)
	, @ResponseSuccess bit = 0
)
AS
BEGIN
	SET NOCOUNT ON;

	IF object_id('tempdb..#TempResult') IS NOT NULL
		drop table #TempResult

	declare @SPName nvarchar(100) = ''

	SELECT @SPName = OBJECT_NAME(@@PROCID)

	BEGIN TRY
		create table #TempResult(IsSuccess bit Default(0), Msg nvarchar(500) Default(''), ID uniqueidentifier)

		IF EXISTS(select * from logs.APIRequest where ID = @ID)
		BEGIN
			update logs.APIRequest set ResponseString = @ResponseString ,ResponseSuccess = @ResponseSuccess 
			where ID = @ID
		END
		ELSE
		BEGIN
			insert into #TempResult(IsSuccess, Msg)
			VALUES(0, '查無此紀錄')
		END

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


