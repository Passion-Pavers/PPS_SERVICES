using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PP.ApplicationService.BusinessService.Contract;
using System.Security.Claims;

namespace PP.ApplicationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationBusinessService _applicationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly string _userName;


        public ApplicationController(IApplicationBusinessService applicationService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _applicationService = applicationService;

            var User = httpContextAccessor.HttpContext?.User;
            _userName = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "AppController";

        }

        [HttpGet]
        [Route("GetActiveApplications")]
        public async Task<IActionResult> GetAllActiveApplications()
        {
            var applications = await _applicationService.GetAllActiveApplications();
            return Ok(applications);
        }

    }
}
