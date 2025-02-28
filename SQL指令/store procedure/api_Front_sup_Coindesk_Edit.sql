USE [Test_cathaybk]
GO

/****** Object:  StoredProcedure [api_Front].[usp_Coindesk_Edit]    Script Date: 2025/2/28 下午 02:32:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [api_Front].[usp_Coindesk_Edit] 
(
	@ID uniqueidentifier
	, @CoinName_Eng varchar(20)
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

		IF EXISTS(select * from Coindesk where ID = @ID)
		BEGIN
			IF NOT EXISTS(select * from Coindesk where CoinName_Eng = @CoinName_Eng and ID <> @ID)
			BEGIN
				IF NOT EXISTS(select * from Coindesk where CoinName_Cht = @CoinName_Cht and ID <> @ID)
				BEGIN
					insert into logs.Coindesk(Coindesk_ID, CoinName_Eng, CoinName_Cht, CreateSPName, CreateDatetime, UpdateSPName, UpdateDatetime, ActionType, ActionSPName)
					select ID, CoinName_Eng, CoinName_Cht, CreateSPName, CreateDatetime, UpdateSPName, UpdateDatetime, 'E', @SPName from Coindesk where ID = @ID

					update Coindesk 
					set CoinName_Eng = @CoinName_Eng, CoinName_Cht = @CoinName_Cht, UpdateDatetime = GetDate(), UpdateSPName = @SPName
					output 1, '修改成功', inserted.ID into #TempResult(IsSuccess, Msg, ID)
					where ID = @ID
				END
				ELSE
				BEGIN
					insert into #TempResult(IsSuccess, Msg)
					VALUES(0, '此幣別中文名稱已存在')
				END
			END
			ELSE
			BEGIN
				insert into #TempResult(IsSuccess, Msg)
				VALUES(0, '此幣別英文名稱已存在')
			END
		END
		ELSE
		BEGIN
			insert into #TempResult(IsSuccess, Msg)
			VALUES(0, '查無此幣別')
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


