using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PP.SampleCRUDService.BusinessService.Contract;
using PP.SampleCRUDService.Models.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PP.SampleCRUDService.Controllers
{
    [ApiController]
    [Route("api/applications")]
    [Authorize]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly string _userName;


        public ApplicationController(IApplicationService applicationService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _applicationService = applicationService;

            var User = httpContextAccessor.HttpContext?.User;
            _userName = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "AppController";
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApplications()
        {
            var applications = await _applicationService.GetAllApplications();
            return Ok(applications);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationById(int id)
        {
            var application = await _applicationService.GetApplicationById(id);
            if (application == null)
                return NotFound();

            return Ok(application);
        }

        [HttpPost]
        public async Task<IActionResult> AddApplication([FromBody] CreateApplicationDto application)
        {
            if (application == null)
            {
                return BadRequest();
            }
            var response = await _applicationService.AddApplication(application, _userName);
            return CreatedAtAction(nameof(GetApplicationById), new { id = response.Id }, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateApplication([FromBody] UpdateApplicationDto application)
        {     
            await _applicationService.UpdateApplication(application, _userName);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            await _applicationService.DeleteApplication(id);
            return Ok();
        }
    }

}
