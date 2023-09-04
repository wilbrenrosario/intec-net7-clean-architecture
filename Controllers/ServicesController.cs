using INTEC.APPLICATION;
using INTEC.CORE.Model;
using INTEC.INFRASTRUCTURE.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace INTEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServices services;
        public ServicesController(IServices services_) { 
            services = services_;
        }

        [HttpGet]
        public async Task<ActionResult<String>> GetMyName()
        {
            return services.MiNombreEs();
        }
    }
}
