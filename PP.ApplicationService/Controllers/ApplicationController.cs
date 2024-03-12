using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PP.ApplicationService.BusinessService.Contract;
using PP.ApplicationService.Models.Dtos;
using System.Security.Claims;

namespace PP.ApplicationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        [HttpPost]
        [Route("CreateApplication")]
        public async Task<IActionResult> AddApplication([FromBody] CreateApplicationDto application)
        {
            if (application == null)
            {
                return BadRequest();
            }
            var response = await _applicationService.AddApplication(application, _userName);
            if (!response.IsSuccess || response.Data == null)
            {
                return StatusCode(500, response.Message); 
            }

            return Ok(response);
        }

        [HttpPost("GetAppConfig")]
        public async Task<IActionResult> GetAppConfig([FromBody] AppConfigRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request body");
            }

            var response = await _applicationService.GetAppConfigJson(request.AppId, request.SubAppId);

            if (!response.IsSuccess || response.Data == null)
            {
                return NotFound(); // SubApp and its parent Application not found
            }

            return Ok(response);
        }

    }
}
