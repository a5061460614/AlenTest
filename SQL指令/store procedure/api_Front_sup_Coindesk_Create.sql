USE [Test_cathaybk]
GO

/****** Object:  StoredProcedure [api_Front].[usp_Coindesk_Create]    Script Date: 2025/2/28 �U�� 02:32:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [api_Front].[usp_Coindesk_Create] 
(
	@CoinName_Eng varchar(20)
	, @CoinName_Cht nvarchar(50)
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

		IF NOT EXISTS(select * from Coindesk where CoinName_Eng = @CoinName_Eng)
		BEGIN
			IF NOT EXISTS(select * from Coindesk where CoinName_Cht = @CoinName_Cht)
			BEGIN
				insert into Coindesk(CoinName_Eng, CoinName_Cht, CreateSPName, UpdateSPName)
				output 1, '�s�W���\', inserted.ID into #TempResult(IsSuccess, Msg, ID)
				VALUES(@CoinName_Eng, @CoinName_Cht, @SPName, @SPName)
			END
			ELSE
			BEGIN
				insert into #TempResult(IsSuccess, Msg)
				VALUES(0, '�����O����W�w�s�b')
			END
		END
		ELSE
		BEGIN
			insert into #TempResult(IsSuccess, Msg)
			VALUES(0, '�����O�^��W�w�s�b')
		END
	END TRY
	BEGIN CATCH

		declare @ErrorMessage nvarchar(500) = ''
		set @ErrorMessage = ERROR_MESSAGE()

		insert into #TempResult(IsSuccess, Msg)
		VALUES(0, @ErrorMessage)

	END CATCH

	select IsSuccess, Msg from #TempResult

END
GO


