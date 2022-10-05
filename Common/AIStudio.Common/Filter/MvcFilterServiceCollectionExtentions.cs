﻿using AIStudio.Common.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Simple.Common.Filters;

namespace AIStudio.Common.Filter
{

    public static class MvcFilterServiceCollectionExtentions
    { 
        /// <summary>
       /// 配置模型验证
       /// </summary>
       /// <param name="builder"></param>
       /// <returns></returns>
        public static IMvcBuilder AddDataValidation(this IMvcBuilder builder)
        {
            builder.ConfigureApiBehaviorOptions(options =>
            {
                // 禁用默认模型验证过滤器
                options.SuppressModelStateInvalidFilter = true;
            });

            builder.AddMvcOptions(options =>
            {
                // 添加自定义模型验证过滤器
                options.Filters.Add<DataValidationFilter>();
            });

            return builder;
        }



        public static IMvcBuilder AddFilter(this IMvcBuilder builder, Action<AjaxResultOptions>? setupAction = null)
        {
            // 添加过滤器
            builder.AddMvcOptions(options =>
            {
                // 过滤器管道（先进后出）顺序：Middleware >> ExceptionFilter >> ActionFilter
                // 异常触发顺序：ActionFilter >> ExceptionFilter

                //添加日志记录
                options.Filters.Add<RequestActionFilter>();
                // ActionFilter 实现，Order = -6000，这个过滤器会标记 AppResultException 被处理，故而 ExceptionFilter 将无法捕捉到
                options.Filters.Add<AjaxResultActionFilter>();
            });

            // 默认 AjaxResultOptions 配置
            builder.Services.AddTransient<IConfigureOptions<AjaxResultOptions>, AjaxResultOptionsSetup>();

            // 如果有自定义配置
            if (setupAction != null) builder.Services.Configure(setupAction);

            return builder;
        }     
    }
}
