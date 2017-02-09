using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using clby.Core.Misc;

namespace clby.Web.Tests.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var v = this.GetValue("k");

            return this.Content("Index");
        }

        public async Task<IActionResult> Load()
        {
            await HttpContext.Session.LoadAsync();

            return this.Content("Load");
        }
        public async Task<IActionResult> Commit()
        {
            await HttpContext.Session.CommitAsync();

            return this.Content("Commit");
        }

        public IActionResult Get()
        {
            var s = HttpContext.Session.GetString("a");
            return this.Content(s);
        }
        public IActionResult Set()
        {
            HttpContext.Session.SetString("a", "a");
            return this.Content("Set");
        }

        public IActionResult Test()
        {
            return this.Content("OK");
        }
    }
}
