﻿using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.BAL.Services;

namespace Ticketing.UnitTests.ServicesTests
{
    public class MemoryCacheAdapterTests
    {
        readonly Dictionary<string, string> myConfiguration = new Dictionary<string, string>
                                                 {
                                                       {"MemoryCacheAdapter:SetSlidingExpiration", "120"},
                                                       {"MemoryCacheAdapter:SetAbsoluteExpiration", "180"}
                                                 };
        IConfigurationRoot? configuration;

        [Fact]
        public void Test_Cache_Set_Get()
        {
            //            var myConfiguration = new Dictionary<string, string>/
            //{
            //    {"MemoryCacheAdapter:SetSlidingExpiration", "120"},
            //   {"MemoryCacheAdapter:SetAbsoluteExpiration", "180"}
            //};

            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //             .AddInMemoryCollection(myConfiguration)
            //             .Build();

            ////      public Test GetSystemUnderTest()
            //    {
            //         var services = new ServiceCollection();
            //           services.AddMemoryCache();
            //         services.AddSingleton<ICacheAdapter, MemoryCacheAdapter>();

            //     var serviceProvider = services.BuildServiceProvider();
            //    var mm = serviceProvider.GetRequiredService<ICacheAdapter>();
            //   var scope = serviceProvider.CreateScope();

            //       var memoryCache = serviceProvider.CreateScope().GetService<ICacheAdapter>();
            //     var memoryCache = scope.ServiceProvider.GetRequiredService<ICacheAdapter>();
            //   scope.Dispose();
            //    return new Test(memoryCache);
            //  }
            //var sp = services.BuildServiceProvider();
            //using (var scope = sp.CreateScope())
            //using (var appContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>())

            string key = "testkey";
            string setValue = "value";
            string getValue = "";

            BuildConfiguration();

            var memoryCache = new MemoryCacheAdapter(new MemoryCache(new MemoryCacheOptions()), configuration);

            memoryCache.Set(key, setValue);
            getValue = memoryCache.Get<string>(key);

            Assert.Equal(getValue, setValue);
        }

        [Fact]
        public void Test_Cache_Set_Inavlidate()
        {
            string key = "testkey";
            string setValue = "value";
            BuildConfiguration();

            var memoryCache = new MemoryCacheAdapter(new MemoryCache(new MemoryCacheOptions()), configuration);

            memoryCache.Set(key, setValue);
            memoryCache.Invalidate(key);
            var result = memoryCache.Get<string>(key);

            result.Should().BeNull();
        }

        private void BuildConfiguration()
        {
            configuration = new ConfigurationBuilder()
           .AddInMemoryCollection(myConfiguration)
           .Build();
        }
    }
}
