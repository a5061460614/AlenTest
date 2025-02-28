using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;


namespace CallAPI.Tests.Controllers
{
    public class coindeskController
    {
        [Fact]
        public async Task Test_Coindesk_Get_Sucess()
        {
            //模擬出一個IHttpClientFactory的假物件
            var mockFactory = new Mock<IHttpClientFactory>();
            //產生http回傳的實例
            HttpResponseMessage result = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };
            //模擬出一個http處理程序的基本類型實例的假物件
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            //模擬http發出請求後所做的一連串事情，最後並回傳結果
            //protected => 發出請求的方法SendAsync是一個protected存取修飾詞，不能直接存取，所以告訴moq請幫忙執行protected方法，詳細說明可看參考6
            //Setup => 設定此執行動作為SendAsync方法，並需要兩個傳入參數，HttpRequestMessage與CancellationToken
            //ItExpr.IsAny => 請moq協助傳入一個參數為HttpRequestMessage與CancellationToken的假參數，另一種為It.IsAny是非protected方法使用
            //ReturnsAsync => 回傳非同步的值
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(result);
            //將製作好的假請求與回傳給HttpClient
            var client = new HttpClient(mockHttpMessageHandler.Object);
            //模擬實際請求，並接收到我們所製作的假物件
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            //將製作好的完整請求流程給我們在使用的方法
            var services = new AlenTest.Controllers.coindeskController(mockFactory.Object);
            //執行要測試的方法
            var response = await services.Coindesk_Get();
            //回傳值不能為null
            Assert.NotNull(response);
            //回傳值必須一致
            Assert.Equal("[{\"IsSuccess\":true,\"Msg\":\"\",\"ID\":\"ecb4ca93-2326-4ad8-b90e-12583beff3df\",\"CoinName_Eng\":\"USD\",\"CoinName_Cht\":\"美金\"},{\"IsSuccess\":true,\"Msg\":\"\",\"ID\":\"d103c93b-b12a-4175-8127-b20f4cfe1cc1\",\"CoinName_Eng\":\"EUR\",\"CoinName_Cht\":\"歐元\"},{\"IsSuccess\":true,\"Msg\":\"\",\"ID\":\"1a221e7e-9d28-49a6-a09b-8970e3134f89\",\"CoinName_Eng\":\"GBP\",\"CoinName_Cht\":\"英鎊\"}]", response);
        }

        [Fact]
        public async Task Test_Coindesk_Create_Sucess()
        {
            //模擬出一個IHttpClientFactory的假物件
            var mockFactory = new Mock<IHttpClientFactory>();
            //產生http回傳的實例
            HttpResponseMessage result = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };

            AlenTest.Controllers.Request RequestObj  = new AlenTest.Controllers.Request();
            RequestObj.CoinName_Cht = "臺幣";
            RequestObj.CoinName_Eng = "TWD";

            //模擬出一個http處理程序的基本類型實例的假物件
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(result);
            //將製作好的假請求與回傳給HttpClient
            var client = new HttpClient(mockHttpMessageHandler.Object);
            //模擬實際請求，並接收到我們所製作的假物件
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            //將製作好的完整請求流程給我們在使用的方法
            var services = new AlenTest.Controllers.coindeskController(mockFactory.Object);
            //執行要測試的方法
            var response = await services.Coindesk_Create(RequestObj);
            //回傳值不能為null
            Assert.NotNull(response);
            //回傳值必須一致
            Assert.Equal("[{\"IsSuccess\":true,\"Msg\":\"新增成功\"}]", response);
        }

        [Fact]
        public async Task Test_Coindesk_Create_Error_1()
        {
            //模擬出一個IHttpClientFactory的假物件
            var mockFactory = new Mock<IHttpClientFactory>();
            //產生http回傳的實例
            HttpResponseMessage result = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };

            AlenTest.Controllers.Request RequestObj = new AlenTest.Controllers.Request();
            RequestObj.CoinName_Cht = "美元";
            RequestObj.CoinName_Eng = "USD";

            //模擬出一個http處理程序的基本類型實例的假物件
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(result);
            //將製作好的假請求與回傳給HttpClient
            var client = new HttpClient(mockHttpMessageHandler.Object);
            //模擬實際請求，並接收到我們所製作的假物件
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            //將製作好的完整請求流程給我們在使用的方法
            var services = new AlenTest.Controllers.coindeskController(mockFactory.Object);
            //執行要測試的方法
            var response = await services.Coindesk_Create(RequestObj);
            //回傳值不能為null
            Assert.NotNull(response);
            //回傳值必須一致
            Assert.Equal("[{\"IsSuccess\":false,\"Msg\":\"此幣別英文名已存在\"}]", response);
        }

        [Fact]
        public async Task Test_Coindesk_Create_Error_2()
        {
            //模擬出一個IHttpClientFactory的假物件
            var mockFactory = new Mock<IHttpClientFactory>();
            //產生http回傳的實例
            HttpResponseMessage result = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };

            AlenTest.Controllers.Request RequestObj = new AlenTest.Controllers.Request();
            RequestObj.CoinName_Cht = "美金";
            RequestObj.CoinName_Eng = "US";

            //模擬出一個http處理程序的基本類型實例的假物件
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(result);
            //將製作好的假請求與回傳給HttpClient
            var client = new HttpClient(mockHttpMessageHandler.Object);
            //模擬實際請求，並接收到我們所製作的假物件
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            //將製作好的完整請求流程給我們在使用的方法
            var services = new AlenTest.Controllers.coindeskController(mockFactory.Object);
            //執行要測試的方法
            var response = await services.Coindesk_Create(RequestObj);
            //回傳值不能為null
            Assert.NotNull(response);
            //回傳值必須一致
            Assert.Equal("[{\"IsSuccess\":false,\"Msg\":\"此幣別中文名已存在\"}]", response);
        }
        [Fact]
        public async Task Test_Coindesk_Edit_Success()
        {
            //模擬出一個IHttpClientFactory的假物件
            var mockFactory = new Mock<IHttpClientFactory>();
            //產生http回傳的實例
            HttpResponseMessage result = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };

            AlenTest.Controllers.Request RequestObj = new AlenTest.Controllers.Request();
            RequestObj.CoinName_Cht = "美金";
            RequestObj.CoinName_Eng = "USD";
            RequestObj.ID = "ECB4CA93-2326-4AD8-B90E-12583BEFF3DF";

            //模擬出一個http處理程序的基本類型實例的假物件
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(result);
            //將製作好的假請求與回傳給HttpClient
            var client = new HttpClient(mockHttpMessageHandler.Object);
            //模擬實際請求，並接收到我們所製作的假物件
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            //將製作好的完整請求流程給我們在使用的方法
            var services = new AlenTest.Controllers.coindeskController(mockFactory.Object);
            //執行要測試的方法
            var response = await services.Coindesk_Edit(RequestObj);
            //回傳值不能為null
            Assert.NotNull(response);
            //回傳值必須一致
            Assert.Equal("[{\"IsSuccess\":true,\"Msg\":\"修改成功\"}]", response);
        }

        [Fact]
        public async Task Test_Coindesk_Edit_Error_1()
        {
            //模擬出一個IHttpClientFactory的假物件
            var mockFactory = new Mock<IHttpClientFactory>();
            //產生http回傳的實例
            HttpResponseMessage result = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };

            AlenTest.Controllers.Request RequestObj = new AlenTest.Controllers.Request();
            RequestObj.CoinName_Cht = "美金";
            RequestObj.CoinName_Eng = "USD";
            RequestObj.ID = "ECB4CA93-2326-4AD8-B90E-12583BEFF3DC";

            //模擬出一個http處理程序的基本類型實例的假物件
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(result);
            //將製作好的假請求與回傳給HttpClient
            var client = new HttpClient(mockHttpMessageHandler.Object);
            //模擬實際請求，並接收到我們所製作的假物件
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            //將製作好的完整請求流程給我們在使用的方法
            var services = new AlenTest.Controllers.coindeskController(mockFactory.Object);
            //執行要測試的方法
            var response = await services.Coindesk_Edit(RequestObj);
            //回傳值不能為null
            Assert.NotNull(response);
            //回傳值必須一致
            Assert.Equal("[{\"IsSuccess\":false,\"Msg\":\"查無此幣別\"}]", response);
        }

        [Fact]
        public async Task Test_Coindesk_Edit_Error_2()
        {
            //模擬出一個IHttpClientFactory的假物件
            var mockFactory = new Mock<IHttpClientFactory>();
            //產生http回傳的實例
            HttpResponseMessage result = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };

            AlenTest.Controllers.Request RequestObj = new AlenTest.Controllers.Request();
            RequestObj.CoinName_Cht = "英鎊";
            RequestObj.CoinName_Eng = "USD";
            RequestObj.ID = "ECB4CA93-2326-4AD8-B90E-12583BEFF3DF";

            //模擬出一個http處理程序的基本類型實例的假物件
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(result);
            //將製作好的假請求與回傳給HttpClient
            var client = new HttpClient(mockHttpMessageHandler.Object);
            //模擬實際請求，並接收到我們所製作的假物件
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            //將製作好的完整請求流程給我們在使用的方法
            var services = new AlenTest.Controllers.coindeskController(mockFactory.Object);
            //執行要測試的方法
            var response = await services.Coindesk_Edit(RequestObj);
            //回傳值不能為null
            Assert.NotNull(response);
            //回傳值必須一致
            Assert.Equal("[{\"IsSuccess\":false,\"Msg\":\"此幣別中文名稱已存在\"}]", response);
        }

        [Fact]
        public async Task Test_Coindesk_Edit_Error_3()
        {
            //模擬出一個IHttpClientFactory的假物件
            var mockFactory = new Mock<IHttpClientFactory>();
            //產生http回傳的實例
            HttpResponseMessage result = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };

            AlenTest.Controllers.Request RequestObj = new AlenTest.Controllers.Request();
            RequestObj.CoinName_Cht = "美金";
            RequestObj.CoinName_Eng = "GBP";
            RequestObj.ID = "ECB4CA93-2326-4AD8-B90E-12583BEFF3DF";

            //模擬出一個http處理程序的基本類型實例的假物件
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(result);
            //將製作好的假請求與回傳給HttpClient
            var client = new HttpClient(mockHttpMessageHandler.Object);
            //模擬實際請求，並接收到我們所製作的假物件
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            //將製作好的完整請求流程給我們在使用的方法
            var services = new AlenTest.Controllers.coindeskController(mockFactory.Object);
            //執行要測試的方法
            var response = await services.Coindesk_Edit(RequestObj);
            //回傳值不能為null
            Assert.NotNull(response);
            //回傳值必須一致
            Assert.Equal("[{\"IsSuccess\":false,\"Msg\":\"此幣別英文名稱已存在\"}]", response);
        }

        [Fact]
        public async Task Test_Coindesk_Delete_Sucess()
        {
            //模擬出一個IHttpClientFactory的假物件
            var mockFactory = new Mock<IHttpClientFactory>();
            //產生http回傳的實例
            HttpResponseMessage result = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };

            AlenTest.Controllers.Request RequestObj = new AlenTest.Controllers.Request();
            RequestObj.ID = "865ACDC3-AC51-4A6A-8F0C-09C97985B38C";

            //模擬出一個http處理程序的基本類型實例的假物件
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(result);
            //將製作好的假請求與回傳給HttpClient
            var client = new HttpClient(mockHttpMessageHandler.Object);
            //模擬實際請求，並接收到我們所製作的假物件
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            //將製作好的完整請求流程給我們在使用的方法
            var services = new AlenTest.Controllers.coindeskController(mockFactory.Object);
            //執行要測試的方法
            var response = await services.Coindesk_Delete(RequestObj);
            //回傳值不能為null
            Assert.NotNull(response);
            //回傳值必須一致
            Assert.Equal("[{\"IsSuccess\":true,\"Msg\":\"刪除成功\"}]", response);
        }

        [Fact]
        public async Task Test_Coindesk_Delete_Error()
        {
            //模擬出一個IHttpClientFactory的假物件
            var mockFactory = new Mock<IHttpClientFactory>();
            //產生http回傳的實例
            HttpResponseMessage result = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };

            AlenTest.Controllers.Request RequestObj = new AlenTest.Controllers.Request();
            RequestObj.ID = "5CD4B2BB-0ECE-47EF-9885-08A0844F34F4";

            //模擬出一個http處理程序的基本類型實例的假物件
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(result);
            //將製作好的假請求與回傳給HttpClient
            var client = new HttpClient(mockHttpMessageHandler.Object);
            //模擬實際請求，並接收到我們所製作的假物件
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            //將製作好的完整請求流程給我們在使用的方法
            var services = new AlenTest.Controllers.coindeskController(mockFactory.Object);
            //執行要測試的方法
            var response = await services.Coindesk_Delete(RequestObj);
            //回傳值不能為null
            Assert.NotNull(response);
            //回傳值必須一致
            Assert.Equal("[{\"IsSuccess\":false,\"Msg\":\"查無此幣別\"}]", response);
        }

        [Fact]
        public async Task Test_Getcurrentprice_Get_Sucess()
        {
            //模擬出一個IHttpClientFactory的假物件
            var mockFactory = new Mock<IHttpClientFactory>();
            //產生http回傳的實例
            HttpResponseMessage result = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };

            //模擬出一個http處理程序的基本類型實例的假物件
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(result);
            //將製作好的假請求與回傳給HttpClient
            var client = new HttpClient(mockHttpMessageHandler.Object);
            //模擬實際請求，並接收到我們所製作的假物件
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            //將製作好的完整請求流程給我們在使用的方法
            var services = new AlenTest.Controllers.coindeskController(mockFactory.Object);
            //執行要測試的方法
            var response = await services.Getcurrentprice();
            //回傳值不能為null
            Assert.NotNull(response);
            //回傳值必須一致
            Assert.Equal("{\"CoinList\":[{\"CoinName_Eng\":\"USD\",\"CoinName_Cht\":\"美金\",\"rate\":23342.012},{\"CoinName_Eng\":\"GBP\",\"CoinName_Cht\":\"英鎊\",\"rate\":19504.398},{\"CoinName_Eng\":\"EUR\",\"CoinName_Cht\":\"歐元\",\"rate\":22738.527}],\"updatedatetime\":\"2022/08/04 04:25:00\"}", response);
        }

        [Fact]
        public async Task Test_AESEncrypt_Sucess()
        {
            //模擬出一個IHttpClientFactory的假物件
            var mockFactory = new Mock<IHttpClientFactory>();
            //產生http回傳的實例
            HttpResponseMessage result = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };

            //模擬出一個http處理程序的基本類型實例的假物件
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(result);
            //將製作好的假請求與回傳給HttpClient
            var client = new HttpClient(mockHttpMessageHandler.Object);
            //模擬實際請求，並接收到我們所製作的假物件
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            //將製作好的完整請求流程給我們在使用的方法
            var services = new AlenTest.Controllers.coindeskController(mockFactory.Object);
            //執行要測試的方法
            var response = await services.AESEncrypt("testStr");
            //回傳值不能為null
            Assert.NotNull(response);
            //回傳值必須一致
            Assert.Equal("Tqij2CelDH0fu6IKmkSGdA==", response);
        }

        [Fact]
        public async Task Test_AESDecrypt_Sucess()
        {
            //模擬出一個IHttpClientFactory的假物件
            var mockFactory = new Mock<IHttpClientFactory>();
            //產生http回傳的實例
            HttpResponseMessage result = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };

            //模擬出一個http處理程序的基本類型實例的假物件
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(result);
            //將製作好的假請求與回傳給HttpClient
            var client = new HttpClient(mockHttpMessageHandler.Object);
            //模擬實際請求，並接收到我們所製作的假物件
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            //將製作好的完整請求流程給我們在使用的方法
            var services = new AlenTest.Controllers.coindeskController(mockFactory.Object);
            //執行要測試的方法
            var response = await services.AESDecrypt("Tqij2CelDH0fu6IKmkSGdA==");
            //回傳值不能為null
            Assert.NotNull(response);
            //回傳值必須一致
            Assert.Equal("testStr", response);
        }
    }

    public class Request
    {
        public string? CoinName_Eng { get; set; }
        public string? CoinName_Cht { get; set; }
        public string? ID { get; set; }
    }
}
