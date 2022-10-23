﻿using AIStudio.Api.Controllers;
using AIStudio.Common.Filter.FilterAttribute;
using AIStudio.Common.Swagger;
using AIStudio.Entity.Base_Manage;
using AIStudio.IBusiness.Base_Manage;
using AIStudio.Util;
using AIStudio.Util.Common;
using Microsoft.AspNetCore.Mvc;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    /// <summary>
    /// 应用密钥
    /// </summary>

    [ApiExplorerSettings(GroupName = nameof(ApiVersionInfo.V1))]
    [Route("/Base_Manage/[controller]/[action]")]
    public class Base_TestController : ApiControllerBase
    {
        #region DI
        IBase_TestBusiness _Base_TestBus { get; }

        /// <summary>
        /// Base_TestController
        /// </summary>
        /// <param name="Base_TestBus"></param>
        public Base_TestController(IBase_TestBusiness Base_TestBus)
        {
            _Base_TestBus = Base_TestBus;
        }
        #endregion

        #region 获取
        /// <summary>
        /// 获取数据列表Base_Test
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<Base_Test>> GetDataList(PageInput input)
        {
            return await _Base_TestBus.GetDataListAsync(input);
        }

        /// <summary>
        /// 获取数据Base_Test
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Base_Test> GetTheData(IdInputDTO input)
        {
            return await _Base_TestBus.GetTheDataAsync(input.id) ?? new Base_Test();
        }

        #endregion

        #region 提交

        /// <summary>
        /// 保存数据Base_Test
        /// </summary>
        /// <param name="theData">保存的数据</param>
        [RequestRecord]
        [HttpPost]
        public async Task SaveData(Base_Test theData)
        {
            if (theData.Id.IsNullOrEmpty())
            {
                await _Base_TestBus.AddDataAsync(theData);
            }
            else
            {
                await _Base_TestBus.UpdateDataAsync(theData);
            }
        }

        /// <summary>
        /// 删除数据Base_Test
        /// </summary>
        /// <param name="ids">id数组,JSON数组</param>
        [RequestRecord]
        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _Base_TestBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}