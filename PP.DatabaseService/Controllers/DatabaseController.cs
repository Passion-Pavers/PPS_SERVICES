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
        private readonly IDataBaseService _dbService;


        public DatabaseController(IDBDataRepo dBDataRepo, IDataBaseService dbService)
        {
                _dBDataRepo = dBDataRepo;
            _dbService = dbService;
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

        [HttpPost("clonetables")]
        public async Task<IActionResult> CloneTablesToNewDatabase(string sourceConnectionString, string targetDatabaseName)
        {
            try
            {
                string dblinkName = "TestDbLink";
                // Create a new database
                await _dbService.CreateNewDatabase(targetDatabaseName);

                // Create DBLink in the new database
                await _dbService.CreateDBLink(sourceConnectionString, targetDatabaseName, dblinkName);

                // Clone all tables with data
                await _dbService.CloneTables(sourceConnectionString, targetDatabaseName, dblinkName);

                return Ok("Tables cloned successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
