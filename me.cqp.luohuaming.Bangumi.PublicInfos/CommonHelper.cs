using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace me.cqp.luohuaming.Bangumi.PublicInfos
{
    public static class CommonHelper
    {
        public const string BaseUrl = "https://api.bgm.tv/";

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }

        public static string GetAppImageDirectory()
        {
            var ImageDirectory = Path.Combine(Environment.CurrentDirectory, "data", "image\\");
            return ImageDirectory;
        }

        public static string ParseLongNumber(int num)
        {
            string numStr = num.ToString();
            int step = 1;
            for (int i = numStr.Length - 1; i > 0; i--)
            {
                if (step % 3 == 0)
                {
                    numStr = numStr.Insert(i, ",");
                }
                step++;
            }
            return numStr;
        }

        public static string GetFileNameFromURL(this string url)
        {
            return url.Split('/').Last().Split('?').First();
        }

        public static string ParseNum2Chinese(this int num)
        {
            return num > 10000 ? $"{num / 10000.0:f1}万" : num.ToString();
        }

        public static bool CompareNumString(string a, string b)
        {
            if (a.Length != b.Length)
            {
                return a.Length > b.Length;
            }

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    return a[i] > b[i];
                }
            }
            return false;
        }

        public static string? Get(string url, string token)
        {
            string result = "";
            try
            {
                url = BaseUrl + url;
                using HttpClient client = new();
                var request = new HttpRequestMessage(new HttpMethod("GET"), url);
                request.Headers.Add("Authorization", $"Bearer {token}");

                HttpResponseMessage response = client.SendAsync(request).Result;
                result = response.Content.ReadAsStringAsync().Result;
                response.EnsureSuccessStatusCode();
                return result;
            }
            catch (Exception ex)
            {
                MainSave.CQLog.Error("发送请求", url + "\n" + result + "\n" + ex.Message + ex.StackTrace);
                return null;
            }
        }

        public static string? Post(string method, string url, string payload, string token)
        {
            string result = "";
            try
            {
                url = BaseUrl + url;
                using HttpClient client = new();
                var request = new HttpRequestMessage(new HttpMethod(method), url)
                {
                    Content = new StringContent(payload, Encoding.UTF8, "application/json")
                };
                request.Headers.Add("Authorization", $"Bearer {token}");

                HttpResponseMessage response = client.SendAsync(request).Result;
                result = response.Content.ReadAsStringAsync().Result;
                response.EnsureSuccessStatusCode();
                return result;
            }
            catch (Exception ex)
            {
                MainSave.CQLog.Error("发送请求", url + "\n" + $"Payload: {payload}\n{result}\n" + ex.Message + ex.StackTrace);
                return null;
            }
        }
    }
}