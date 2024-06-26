﻿using AIStudio.Util.Common;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIStudio.Client.Business
{

    public interface IDataProvider    {

        string Url { get; set; }

        TimeSpan TimeOut { get; set; }

        Task<Dictionary<string, string>> GetHeader();
        Task<AjaxResult<string>> GetToken(string userName, string password);

        Task<AjaxResult> ClearToken();
     
        Task<AjaxResult<T>> PostData<T>(string url, Dictionary<string, string> data);
     
        Task<AjaxResult<T>> PostData<T>(string url, string json = "{}");

        Task<AjaxResult<T>> PostData<T>(string url, object data);

        Task<AjaxResult<T>> GetData<T>(string url, Dictionary<string, string> data);

        Task<UploadResult> UploadFileByForm(string path);

        Task<UploadResult> UploadFileByForm(IBrowserFile file);
    }
}
