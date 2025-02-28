using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.Intrinsics.Arm;

namespace AlenTest
{
    public class coinInfo
    {
        public timeInfo? time { get; set; }

        public string? disclaimer { get; set; }
        public string? chartName { get; set; }

        public bpiInfo? bpi { get; set; }
        public string? CoinChtName { get; set; }
        public string? CoinEngName { get; set; }

        public class CoinDeskAPIResponse
        {
            public string? updatedatetime { get; set; }

            public List<CoinList>? CoinList = new List<CoinList>();
        }

        public class CoinList
        {
            public string? CoinName_Eng { get; set; }
            public string? CoinName_Cht { get; set; }
            public float? rate { get; set; }
        }

    }

    public class timeInfo
    {
        public string? updated { get; set; }
        public string? updatedISO { get; set; }
        public string? updateduk { get; set; }
    }

    public class bpiInfo
    {
        public DolorInfo? USD { get; set; }
        public DolorInfo? GBP { get; set; }
        public DolorInfo? EUR { get; set; }
    }

    public class DolorInfo
    {
        public string? code { get; set; }
        public string? symbol { get; set; }
        public string? rate { get; set; }
        public string? description { get; set; }
        public float? rate_float { get; set; }
    }

    public class CoinData_Maintain
    {
        private string ConnStr = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Test_cathaybk;User ID=Alen;Password=07010511;TrustServerCertificate=true";

        public DataTable Coindesk_Get()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("api_Front.usp_Coindesk_Get", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                var dataReader = cmd.ExecuteReader();
                dt.Load(dataReader);
                conn.Close();
            }
            return dt;
        }

        public DataTable Coindesk_Create(string CoinName_Eng, String CoinName_Cht)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("api_Front.usp_Coindesk_Create", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CoinName_Eng", CoinName_Eng);
                cmd.Parameters.AddWithValue("@CoinName_Cht", CoinName_Cht);
                var dataReader = cmd.ExecuteReader();
                dt.Load(dataReader);
                conn.Close();
            }
            return dt;
        }

        public DataTable Coindesk_Edit(string ID, string CoinName_Eng, String CoinName_Cht)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("api_Front.usp_Coindesk_Edit", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@CoinName_Eng", CoinName_Eng);
                cmd.Parameters.AddWithValue("@CoinName_Cht", CoinName_Cht);
                var dataReader = cmd.ExecuteReader();
                dt.Load(dataReader);
                conn.Close();
            }
            return dt;
        }

        public DataTable Coindesk_Delete(string ID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("api_Front.usp_Coindesk_Delete", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                var dataReader = cmd.ExecuteReader();
                dt.Load(dataReader);
                conn.Close();
            }
            return dt;
        }

        public DataTable RequestLog_Create(string APIPath, string RequestBody, string UserHostAddress, string UserAgent, string UrlReferrer)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("api_Front.usp_APIRequest_Create", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@APIPath", APIPath);
                if (!string.IsNullOrEmpty(RequestBody))
                    cmd.Parameters.AddWithValue("@RequestBody", RequestBody);
                if (!string.IsNullOrEmpty(UserHostAddress))
                    cmd.Parameters.AddWithValue("@UserHostAddress", UserHostAddress);
                if (!string.IsNullOrEmpty(UserAgent))
                    cmd.Parameters.AddWithValue("@UserAgent", UserAgent);
                if (!string.IsNullOrEmpty(UrlReferrer))
                    cmd.Parameters.AddWithValue("@UrlReferrer", UrlReferrer);
                var dataReader = cmd.ExecuteReader();
                dt.Load(dataReader);
                conn.Close();
            }

            return dt;
        }

        public DataTable RequestLog_Get()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("api_Front.usp_APIRequest_Get", conn);
                
                var dataReader = cmd.ExecuteReader();
                dt.Load(dataReader);
                conn.Close();
            }

            return dt;
        }

        public DataTable RequestLog_Update(string ID, string ResponseString, string ResponseSuccess)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("api_Front.usp_APIRequest_Update", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                if (!string.IsNullOrEmpty(ResponseString))
                    cmd.Parameters.AddWithValue("@ResponseString", ResponseString);
                cmd.Parameters.AddWithValue("@ResponseSuccess", ResponseSuccess);
                var dataReader = cmd.ExecuteNonQuery();
                conn.Close();
            }

            return dt;
        }

        public string AESEncrypt(string Str)
        {
            string encrypt = "";
            try
            {
                var aes = System.Security.Cryptography.Aes.Create();
                var md5 = MD5.Create();
                var sha256 = SHA256.Create();
                byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes("UxJwaH3=1-KP9:)fuTEC"));
                byte[] iv = md5.ComputeHash(Encoding.UTF8.GetBytes("UxJwaH3=1-KP9:)fuTEC"));
                aes.Key = key;
                aes.IV = iv;

                byte[] dataByteArray = Encoding.UTF8.GetBytes(Str);
                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataByteArray, 0, dataByteArray.Length);
                    cs.FlushFinalBlock();
                    encrypt = Convert.ToBase64String(ms.ToArray());
                }
            }
            catch (Exception e)
            {
                encrypt = "加密失敗";
            }
            return encrypt;
        }

        public string AESDecrypt(string Str)
        {
            string decrypt = "";
            try
            {

                var aes = System.Security.Cryptography.Aes.Create();
                var md5 = MD5.Create();
                var sha256 = SHA256.Create();
                byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes("UxJwaH3=1-KP9:)fuTEC"));
                byte[] iv = md5.ComputeHash(Encoding.UTF8.GetBytes("UxJwaH3=1-KP9:)fuTEC"));
                aes.Key = key;
                aes.IV = iv;

                byte[] dataByteArray = Convert.FromBase64String(Str);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(dataByteArray, 0, dataByteArray.Length);
                        cs.FlushFinalBlock();
                        decrypt = Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception e)
            {
                decrypt = "加密失敗";
            }
            return decrypt;
        }
    }
}
