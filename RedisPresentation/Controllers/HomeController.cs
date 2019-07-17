using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using RedisPresentation.Cache;
using RedisPresentation.Models;

namespace RedisPresentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly ICacheProvider _cacheProvider;

        public HomeController(IDistributedCache cache, ICacheProvider cacheProvider)
        {
            _cache = cache;
            _cacheProvider = cacheProvider;
        }

        public async Task<IActionResult> Index()
        {
            var value = HttpContext.Session.GetString("key");
            var cache = await _cache.GetStringAsync("distributed-key");

            if (await _cacheProvider.ExistsAsync("custom-distributed-key"))
            {
                var customCache = await _cacheProvider.GetAsync<string>("custom-distributed-key");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            var value = Guid.NewGuid().ToString();
            HttpContext.Session.SetString("key", Guid.NewGuid().ToString());
            return View();
        }

        public async Task<IActionResult> CacheStuff()
        {
            await _cache.SetStringAsync("distributed-key", Guid.NewGuid().ToString());

            await _cacheProvider.SetAsync("custom-distributed-key", Guid.NewGuid().ToString());
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
