using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PP.Services.Data;
using PP.Services.Models;
using Logger= PP.Services.Logger;

namespace PP.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TestsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostString([FromBody] TestInput value)
        {
            var entity = new Test { textboxname = value.textboxname };

            Logger.Logger logger = new Logger.Logger();
            logger.Log(value);

            _context.StringEntities.Add(entity);
            await _context.SaveChangesAsync();

            return Ok(new { Id = entity.id });
        }
    }
}
