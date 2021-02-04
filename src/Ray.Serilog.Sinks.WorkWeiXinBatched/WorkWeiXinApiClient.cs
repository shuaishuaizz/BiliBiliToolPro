﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Ray.Serilog.Sinks.Batched;

namespace Ray.Serilog.Sinks.WorkWeiXinBatched
{
    public class WorkWeiXinApiClient : IPushService
    {
        private readonly Uri _apiUrl;
        private readonly HttpClient _httpClient = new HttpClient();

        public WorkWeiXinApiClient(string webHookUrl)
        {
            _apiUrl = new Uri(webHookUrl);
        }

        public override string Name => "企业微信";

        public override async Task<HttpResponseMessage> PushMessageAsync(string message)
        {
            await base.PushMessageAsync(message);

            var json = new { msgtype = "markdown", markdown = new { content = message } }.ToJson();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_apiUrl, content);
            return response;
        }
    }
}
