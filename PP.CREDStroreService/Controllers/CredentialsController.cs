using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PP.CREDStroreService.BusinessService.Contract;
using PP.CREDStroreService.Models.DbEntities;
using PP.CREDStroreService.Models.Dtos;
using PP.CREDStroreService.Repository.Contract;
using System.Security.Claims;

namespace PP.CREDStroreService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CredentialsController : ControllerBase
    {
       
            private readonly ICredStoreBusinessService _credentialBusinessService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly string _userName;

        public CredentialsController(ICredStoreBusinessService credentialBusinessService, IHttpContextAccessor httpContextAccessor)
        {
            _credentialBusinessService = credentialBusinessService;
            _httpContextAccessor = httpContextAccessor;
            var User = httpContextAccessor.HttpContext?.User;
            _userName = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "CredStoreController";
        }

        // GET: api/credentials
            [HttpPost]
            [Route("GetCredentials")]
            public async Task<ActionResult> GetCredentials([FromBody] GetCredentialsDto credRequest)
            {
                var credentials = await _credentialBusinessService.GetAllAsync(credRequest);
                return Ok(credentials);
            }

            //// GET: api/credentials/5
            //[HttpGet("{id}")]
            //public async Task<ActionResult<ResponseDto>> GetCredential(int id)
            //{
            //    var credential = await _credentialBusinessService.GetByIdAsync(id);

            //    if (credential == null)
            //    {
            //        return NotFound();
            //    }

            //    return credential;
            //}

            // POST: api/credentials
            [HttpPost]
            [Route("AddCredentials")]
            public async Task<ActionResult<ResponseDto>> PostCredential(CredentialsDto credential)
            {
                var response = await _credentialBusinessService.AddAsync(credential,_userName);
                return response;
            }

            // PUT: api/credentials/5
            [HttpPut("{id}")]
            public async Task<IActionResult> PutCredential(int id, UpdateCredentialDto credential)
            {
                if (id != credential.Id)
                {
                    return BadRequest();
                }
               var response =  await _credentialBusinessService.UpdateAsync(credential, _userName );
                return Ok(response);
            }

            // DELETE: api/credentials/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteCredential(int id, DeleteCredentialsDto deleteCreds)
            {               

                if (id == 0)
                {
                    return NotFound();
                }
                var response = await _credentialBusinessService.DeleteAsync(id, deleteCreds);
                return Ok(response);
            }
        }
}
