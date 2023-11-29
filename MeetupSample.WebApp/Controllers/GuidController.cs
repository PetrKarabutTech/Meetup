using System;
using Microsoft.AspNetCore.Mvc;

namespace MeetupSample.WebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GuidController : ControllerBase
    {
        [HttpGet]
        public string GetGuid()
        {
            return Guid.NewGuid().ToString().ToUpper();
        }
    }
}
