USE [Test_cathaybk]
GO

/****** Object:  StoredProcedure [api_Front].[usp_Coindesk_Get]    Script Date: 2025/2/28 ¤U¤È 02:33:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [api_Front].[usp_Coindesk_Get] 
AS
BEGIN
	SET NOCOUNT ON;

	IF object_id('tempdb..#TempResult') IS NOT NULL
		drop table #TempResult

	declare @SPName nvarchar(100) = ''

	SELECT @SPName = OBJECT_NAME(@@PROCID)

	BEGIN TRY
		create table #TempResult(IsSuccess bit Default(0), Msg nvarchar(100) Default(''), ID uniqueidentifier, CoinName_Eng varchar(20), CoinName_Cht nvarchar(50), UpdateDatetime datetime)

		insert into #TempResult(IsSuccess, Msg, ID, CoinName_Eng, CoinName_Cht, UpdateDatetime)
		select 1, '', ID, CoinName_Eng, CoinName_Cht, UpdateDatetime from Coindesk


	END TRY
	BEGIN CATCH

		declare @ErrorMessage nvarchar(500) = ''
		set @ErrorMessage = ERROR_MESSAGE()

		insert into #TempResult(IsSuccess, Msg)
		VALUES(0, @ErrorMessage)

	END CATCH

	select IsSuccess, Msg, ID, CoinName_Eng, CoinName_Cht from #TempResult
	order by UpdateDatetime desc
END
GO


