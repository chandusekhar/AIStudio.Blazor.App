﻿using AIStudio.Common.AppSettings;
using AIStudio.Common.Authentication.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIStudio.Common.Cache
{
    public static class CacheServiceCollectionExtensions
    {
        /// <summary>
        /// 注册缓存服务，如有配置 Redis 则启用，没有则启用分布式内存缓存（DistributedMemoryCache）
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCache(this IServiceCollection services)
        {
            // 根据情况，启用 Redis 或 DistributedMemoryCache
            if (AppSettingsConfig.RedisOptions.Enabled)
            {
                services.AddSingleton<ICache, RedisCache> ();
            }
            else
            {
                services.AddMemoryCache();
                services.AddSingleton<ICache, MemoryCache>();
            }
            return services;
        }
    }
}