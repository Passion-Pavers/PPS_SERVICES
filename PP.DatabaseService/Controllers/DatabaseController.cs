using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PP.DatabaseService.Models.Dtos;
using PP.DatabaseService.Repository.Contract;

namespace PP.DatabaseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly IDBDataRepo _dBDataRepo;
        public DatabaseController(IDBDataRepo dBDataRepo)
        {
                _dBDataRepo = dBDataRepo;   
        }

        [HttpPost("cloneDataBase")]
        public IActionResult CloneDatabase([FromBody] CloneDatabaseDto model)
        { 

            try
            {
               var response =  _dBDataRepo.CloneDatabase(model.ExistingDatabaseConnectionString, model.NewDatabaseName);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
