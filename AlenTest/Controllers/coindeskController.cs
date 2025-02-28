using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.SignalR.Protocol;
using static AlenTest.coinInfo;
using System.Reflection;
using System.Data;
using Microsoft.IdentityModel.Tokens;
using Azure.Core;
using Microsoft.Data.SqlClient;
using Azure;
using System.IO;

namespace AlenTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class coindeskController : ControllerBase
    {
        readonly IHttpClientFactory _httpClientFactory;
        public coindeskController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// 取得Request Log清單
        /// </summary>
        /// <returns></returns>
        [Route("api/[controller]/RequestLog_Get")]
        [HttpGet]
        public async Task<string> RequestLog_Get()
        {

            RequestInfo RequestInfo = new RequestInfo();
            RequestInfo = GetRequestInfo();

            DataTable dt = new DataTable();
            CoinData_Maintain Con = new CoinData_Maintain();

            //紀錄呼叫Log
            //DataTable dt_Log = Con.RequestLog_Create(RequestInfo.APIPath, "", RequestInfo.UserHostAddress, RequestInfo.UserAgent, RequestInfo.UrlReferrer);

            //呼叫DB
            dt = Con.RequestLog_Get();

            //更新Log狀態
            //Con.RequestLog_Update(dt_Log.Rows[0]["ID"].ToString(), JsonConvert.SerializeObject(dt), "1");
            return JsonConvert.SerializeObject(dt);
        }

        /// <summary>
        /// 取得幣別清單
        /// </summary>
        /// <returns></returns>
        [Route("api/[controller]/Coindesk_Get")]
        [HttpGet]
        public async Task<string> Coindesk_Get()
        {

            RequestInfo RequestInfo = new RequestInfo();
            RequestInfo = GetRequestInfo();

            DataTable dt = new DataTable();
            DataTable dt_Log = new DataTable();
            CoinData_Maintain Con = new CoinData_Maintain();
            
            //紀錄呼叫Log
            if (!string.IsNullOrEmpty(RequestInfo.APIPath))
                dt_Log = Con.RequestLog_Create(RequestInfo.APIPath, "", RequestInfo.UserHostAddress, RequestInfo.UserAgent, RequestInfo.UrlReferrer);
            
            //呼叫DB
            dt = Con.Coindesk_Get();

            //更新Log狀態
            if(dt_Log.Rows.Count > 0)
                Con.RequestLog_Update(dt_Log.Rows[0]["ID"].ToString(), JsonConvert.SerializeObject(dt), "1");
            return JsonConvert.SerializeObject(dt);
        }

        /// <summary>
        /// 新增幣別
        /// </summary>
        /// <param name="RequestObj"></param>
        /// <returns></returns>
        [Route("api/[controller]/Coindesk_Create")]
        [HttpPost]
        public async Task<string> Coindesk_Create(Request RequestObj)
        {

            RequestInfo RequestInfo = new RequestInfo();
            RequestInfo = GetRequestInfo();

            DataTable dt = new DataTable();
            DataTable dt_Log = new DataTable();
            CoinData_Maintain Con = new CoinData_Maintain();
            if (!string.IsNullOrEmpty(RequestInfo.APIPath))
                dt_Log = Con.RequestLog_Create(RequestInfo.APIPath, JsonConvert.SerializeObject(RequestObj), RequestInfo.UserHostAddress, RequestInfo.UserAgent, RequestInfo.UrlReferrer);

            if (!string.IsNullOrEmpty(RequestObj.CoinName_Cht) && !string.IsNullOrEmpty(RequestObj.CoinName_Eng))
                dt = Con.Coindesk_Create(RequestObj.CoinName_Eng, RequestObj.CoinName_Cht);

            if (dt_Log.Rows.Count > 0)
                Con.RequestLog_Update(dt_Log.Rows[0]["ID"].ToString(), JsonConvert.SerializeObject(dt), "1");

            return JsonConvert.SerializeObject(dt);
        }

        /// <summary>
        /// 修改幣別
        /// </summary>
        /// <param name="RequestObj"></param>
        /// <returns></returns>
        [Route("api/[controller]/Coindesk_Edit")]
        [HttpPatch]
        public async Task<string> Coindesk_Edit(Request RequestObj)
        {

            RequestInfo RequestInfo = new RequestInfo();
            RequestInfo = GetRequestInfo();

            DataTable dt = new DataTable();
            DataTable dt_Log = new DataTable();
            CoinData_Maintain Con = new CoinData_Maintain();
            if (!string.IsNullOrEmpty(RequestInfo.APIPath))
                dt_Log = Con.RequestLog_Create(RequestInfo.APIPath, JsonConvert.SerializeObject(RequestObj), RequestInfo.UserHostAddress, RequestInfo.UserAgent, RequestInfo.UrlReferrer);

            if (!string.IsNullOrEmpty(RequestObj.CoinName_Cht) && !string.IsNullOrEmpty(RequestObj.CoinName_Eng) && !string.IsNullOrEmpty(RequestObj.ID))
                dt = Con.Coindesk_Edit(RequestObj.ID, RequestObj.CoinName_Eng, RequestObj.CoinName_Cht);

            if (dt_Log.Rows.Count > 0)
                Con.RequestLog_Update(dt_Log.Rows[0]["ID"].ToString(), JsonConvert.SerializeObject(dt), "1");

            return JsonConvert.SerializeObject(dt);
        }

        /// <summary>
        /// 刪除幣別
        /// </summary>
        /// <param name="RequestObj"></param>
        /// <returns></returns>
        [Route("api/[controller]/Coindesk_Delete")]
        [HttpDelete]
        public async Task<string> Coindesk_Delete(Request RequestObj)
        {

            RequestInfo RequestInfo = new RequestInfo();
            RequestInfo = GetRequestInfo();

            DataTable dt = new DataTable();
            DataTable dt_Log = new DataTable();
            CoinData_Maintain Con = new CoinData_Maintain();
            if (!string.IsNullOrEmpty(RequestInfo.APIPath))
                dt_Log = Con.RequestLog_Create(RequestInfo.APIPath, JsonConvert.SerializeObject(RequestObj), RequestInfo.UserHostAddress, RequestInfo.UserAgent, RequestInfo.UrlReferrer);

            if (!string.IsNullOrEmpty(RequestObj.ID))
                dt = Con.Coindesk_Delete(RequestObj.ID);
            if (dt_Log.Rows.Count > 0)
                Con.RequestLog_Update(dt_Log.Rows[0]["ID"].ToString(), JsonConvert.SerializeObject(dt), "1");

            return JsonConvert.SerializeObject(dt);
        }

        [Route("api/[controller]/AESEncrypt")]
        [HttpGet]
        public async Task<string> AESEncrypt(string str)
        {

            RequestInfo RequestInfo = new RequestInfo();
            RequestInfo = GetRequestInfo();
            DataTable dt_Log = new DataTable();
            CoinData_Maintain Con = new CoinData_Maintain();
            if (!string.IsNullOrEmpty(RequestInfo.APIPath))
                dt_Log = Con.RequestLog_Create(RequestInfo.APIPath, str, RequestInfo.UserHostAddress, RequestInfo.UserAgent, RequestInfo.UrlReferrer);

            var EncryptStr = Con.AESEncrypt(str);


            if (dt_Log.Rows.Count > 0)
                Con.RequestLog_Update(dt_Log.Rows[0]["ID"].ToString(), EncryptStr, "1");

            return EncryptStr;
        }

        [Route("api/[controller]/AESDecrypt")]
        [HttpGet]
        public async Task<string> AESDecrypt(string str)
        {

            RequestInfo RequestInfo = new RequestInfo();
            RequestInfo = GetRequestInfo();
            DataTable dt_Log = new DataTable();
            CoinData_Maintain Con = new CoinData_Maintain();
            if (!string.IsNullOrEmpty(RequestInfo.APIPath))
                dt_Log = Con.RequestLog_Create(RequestInfo.APIPath, str, RequestInfo.UserHostAddress, RequestInfo.UserAgent, RequestInfo.UrlReferrer);

            var DecryptStr = Con.AESDecrypt(str);


            if (dt_Log.Rows.Count > 0)
                Con.RequestLog_Update(dt_Log.Rows[0]["ID"].ToString(), DecryptStr, "1");

            return DecryptStr;
        }

        /// <summary>
        /// 呼叫CoinDesk API
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> Getcurrentprice()
        {
            RequestInfo RequestInfo = new RequestInfo();
            RequestInfo = GetRequestInfo();
            HttpResponseMessage response = new HttpResponseMessage();
            HttpClient? client = new HttpClient();
            coinInfo? ResponseResult = new coinInfo();
            string ResponseStr = "";

            CoinData_Maintain Con = new CoinData_Maintain();
            DataTable dt_Log = Con.RequestLog_Create("https://api.coindesk.com/v1/bpi/currentprice.json", "", RequestInfo.UserHostAddress, RequestInfo.UserAgent, RequestInfo.UrlReferrer);
            try
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await client.GetAsync("https://api.coindesk.com/v1/bpi/currentprice.json");
                if (response.IsSuccessStatusCode)
                {
                    var Result = response.Content.ReadAsStringAsync();
                    ResponseResult = JsonConvert.DeserializeObject<coinInfo>(Result.Result);
                    ResponseStr = GetResponseStr(ResponseResult);
                    client.Dispose();
                }
                else
                {
                    ResponseStr = await ReadCoinDeskByJSONFile();
                }

            }
            catch (HttpRequestException exp)
            {
                Con.RequestLog_Update(dt_Log.Rows[0]["ID"].ToString(), exp.Message, "0");
                ResponseStr = await ReadCoinDeskByJSONFile();
            }
            
            return ResponseStr;
        }

        /// <summary>
        /// 將DeskAPI回傳的結果轉換成要輸出的字串
        /// </summary>
        /// <param name="coinInfo"></param>
        /// <returns></returns>
        private String GetResponseStr(coinInfo? coinInfo)
        {
            string? ResponseStr = "";
            CoinDeskAPIResponse ResponseObj = new CoinDeskAPIResponse();
            if (!string.IsNullOrEmpty(coinInfo.time.updatedISO))
            {
                ResponseObj.updatedatetime = Convert.ToDateTime(coinInfo.time.updatedISO).ToString("yyyy/MM/dd HH:mm:ss");
            }
            var ClassInfo = coinInfo.bpi.GetType().GetProperties();

            DataTable dt = new DataTable();
            CoinData_Maintain Con = new CoinData_Maintain();
            dt = Con.Coindesk_Get();

            foreach (var Properties in ClassInfo)
            {
                CoinList Coin = new CoinList();
                Coin.CoinName_Eng = Properties.Name;
                Coin.rate = (float)Properties.GetValue(coinInfo.bpi).GetType().GetProperty("rate_float").GetValue(Properties.GetValue(coinInfo.bpi));

                //用英文名稱去撈對應的中文名稱
                DataRow[] dr = dt.Select("CoinName_Eng='" + Coin.CoinName_Eng + "'");
                if (dr.Length > 0)
                    Coin.CoinName_Cht = dr[0]["CoinName_Cht"].ToString();

                ResponseObj.CoinList.Add(Coin);
            }
            ResponseStr = JsonConvert.SerializeObject(ResponseObj);
            return ResponseStr;
        }

        /// <summary>
        /// API網址無法使用，改成讀取JSON檔
        /// </summary>
        /// <returns></returns>
        private async Task<string> ReadCoinDeskByJSONFile()
        {

            RequestInfo RequestInfo = new RequestInfo();
            RequestInfo = GetRequestInfo();

            CoinData_Maintain Con = new CoinData_Maintain();
            DataTable dt_Log = Con.RequestLog_Create("ReadCoinDeskByJSONFile", "", RequestInfo.UserHostAddress, RequestInfo.UserAgent, RequestInfo.UrlReferrer);
            coinInfo? ResponseResult = new coinInfo();
            string ResponseStr = "";

            string filePath = "File/MockingData.json";
            string jsonContent = System.IO.File.ReadAllText(filePath);
            ResponseResult = JsonConvert.DeserializeObject<coinInfo>(jsonContent);

            ResponseStr = GetResponseStr(ResponseResult);

            Con.RequestLog_Update(dt_Log.Rows[0]["ID"].ToString(), ResponseStr, "1");

            return ResponseStr;
        }

        /// <summary>
        /// 取得IP、Device、Referrer
        /// </summary>
        /// <returns></returns>
        private RequestInfo GetRequestInfo()
        {
            RequestInfo RequestInfo = new RequestInfo();
            if (Request != null)
            {
                RequestInfo.UserHostAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                RequestInfo.UserAgent = Request.Headers["User-Agent"].ToString();
                RequestInfo.UrlReferrer = Request.Headers["Referer"].ToString();
                RequestInfo.APIPath = Request.Path;
            }

            return RequestInfo;
        }
    }

    public class Request
    {
        public string? CoinName_Eng { get; set; }
        public string? CoinName_Cht { get; set; }
        public string? ID { get; set; }
    }

    public class RequestInfo
    {
        public string? UserHostAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? UrlReferrer { get; set; }
        public string? APIPath { get; set; }
    }

}
