using System;
using Microsoft.AspNetCore.Mvc;

namespace Challenge3.Controllers
{
    [Route("api/[controller]")]
    public class ServerTimeController : Controller
    {
        [HttpGet("[action]")]
        public string LongTimeString()
        {
            var dt = DateTime.Now;
            return $"{dt.ToShortDateString()} {dt.ToLongTimeString()}"; ;
        }
    }
}
