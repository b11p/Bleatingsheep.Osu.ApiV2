using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bleatingsheep.Osu.ApiV2.Utils
{
    public static class HttpClientUtil
    {
        internal static int Timeout { get; set; } = 5000;
        public static int RetryCount { get; set; } = 3;
        private static readonly HttpClient Http;

        static HttpClientUtil()
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip
            };
            ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
            Http = new HttpClient(handler)
            {
                Timeout = new TimeSpan(0, 0, 0, 0, Timeout)
            };
        }

        /// <summary>
        /// Post without parameters
        /// </summary>
        /// <param name="url">HTTP url</param>
        /// <returns></returns>
        public static HttpResponseMessage HttpPost(string url)
        {
            HttpContent content = new StringContent("");
            content.Headers.ContentType = new MediaTypeHeaderValue(HttpContentType.Form.GetContentType());
            return HttpPost(url, content);
        }

        /// <summary>
        /// Post with JSON
        /// </summary>
        /// <param name="url">HTTP url</param>
        /// <param name="postJson">JSON string</param>
        /// <returns></returns>
        public static HttpResponseMessage HttpPost(string url, string postJson)
        {
            HttpContent content = new StringContent(postJson);
            content.Headers.ContentType = new MediaTypeHeaderValue(HttpContentType.Json.GetContentType());
            return HttpPost(url, content);
        }

        /// <summary>
        /// Post with JSON
        /// </summary>
        /// <param name="url">HTTP url</param>
        /// <param name="jsonObject">Object which will be auto-converted to json string.</param>
        /// <returns></returns>
        public static HttpResponseMessage HttpPost(string url, object jsonObject)
        {
            HttpContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject));
            content.Headers.ContentType = new MediaTypeHeaderValue(HttpContentType.Json.GetContentType());
            return HttpPost(url, content);
        }

        /// <summary>
        /// Post with JSON
        /// </summary>
        /// <param name="url">HTTP url</param>
        /// <param name="args">Parameters dictionary</param>
        /// <param name="argsHeader">Header dictionary</param>
        /// <returns></returns>
        public static HttpResponseMessage HttpPost(string url, IDictionary<string, string> args,
            IDictionary<string, string> argsHeader = null)
        {
            HttpContent content;
            if (args != null)
            {
                var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(args);
                content = new StringContent(jsonStr);
                content.Headers.ContentType = new MediaTypeHeaderValue(HttpContentType.Json.GetContentType());
            }
            else
            {
                content = new StringContent("");
                content.Headers.ContentType = new MediaTypeHeaderValue(HttpContentType.Form.GetContentType());
            }

            if (argsHeader != null)
            {
                foreach (var item in argsHeader)
                    content.Headers.Add(item.Key, item.Value);
            }

            return HttpPost(url, content);
        }

        /// <summary>
        /// Get request
        /// </summary>
        /// <param name="url">HTTP url</param>
        /// <param name="args">Parameters dictionary</param>
        /// <param name="argsHeader">Header dictionary</param>
        /// <returns></returns>
        public static HttpResponseMessage HttpGet(string url, IDictionary<string, string> args = null,
            IDictionary<string, string> argsHeader = null)
        {
            for (int i = 0; i < RetryCount; i++)
            {
                try
                {
                    if (args != null)
                    {
                        url = url + args.ToUrlParamString();
                    }

                    var message = new HttpRequestMessage(HttpMethod.Get, url);
                    if (argsHeader != null)
                    {
                        foreach (var item in argsHeader)
                        {
                            message.Headers.Add(item.Key, item.Value);
                        }
                    }
                    CancellationTokenSource cts = new CancellationTokenSource(Timeout);
                    HttpResponseMessage response = Http.SendAsync(message, cts.Token).Result;

                    return response;
                }
                catch (Exception)
                {
                    Console.WriteLine($"Timed out. Retried {i + 1} times.");
                    if (i == RetryCount - 1)
                        throw;
                }
            }

            return null;
        }

        private static HttpResponseMessage HttpPost(string url, HttpContent content)
        {
            for (int i = 0; i < RetryCount; i++)
            {
                try
                {
                    var response = Http.PostAsync(url, content).Result;
                    //response.EnsureSuccessStatusCode();
                    return response;
                }
                catch (Exception)
                {
                    Console.WriteLine($"Timed out. Retried {i + 1} times.");
                    if (i == RetryCount - 1)
                        throw;
                }
            }

            return null;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors errors)
        {
            return true; // always accept
        }

        private static string GetContentType(this HttpContentType type)
        {
            switch (type)
            {
                case HttpContentType.Json:
                    return "application/json";
                default:
                case HttpContentType.Form:
                    return "application/x-www-form-urlencoded";
            }
        }

        private enum HttpContentType
        {
            Json,
            Form
        }
    }
}
