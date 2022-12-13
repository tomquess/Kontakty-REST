using KontaktyBackend.Helpers;
using KontaktyBackend.Models;
using KontaktyBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KontaktyBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KontaktController : ControllerBase
    {

        private IUserService _userService;
        private readonly KontaktyDbContext _dbContext;
        private IConfiguration _config;


        public KontaktController(IConfiguration config, KontaktyDbContext dbContext, IUserService userService)
        {
            _config = config;
            _dbContext = dbContext;
            _userService = userService;
        }

        [AuthorizeUser]
        [HttpGet]
        public async Task<ActionResult<List<KontaktModel>>> Get()
        {
            
            return Ok(await _dbContext.Kontakty.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KontaktModel>> Get(int id)
        {
            var kontakt = await _dbContext.Kontakty.FindAsync(id);
            if (kontakt == null)
                return BadRequest("Brak takiego kontaku.");
            return Ok(kontakt);
        }

        [HttpPost]
        public async Task<ActionResult<List<KontaktModel>>> AddKontakt(KontaktModel kontakt)
        {
            _dbContext.Kontakty.Add(kontakt);
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.Kontakty.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<KontaktModel>>> UpdateKontakt(KontaktModel request)
        {
            var dbKontakt = await _dbContext.Kontakty.FindAsync(request.Id);
            if (dbKontakt == null)
                return BadRequest("Brak takiego kontaku.");

            dbKontakt.Imie = request.Imie;

            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.Kontakty.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<KontaktModel>> Delete(int id)
        {
            var dbKontakt = await _dbContext.Kontakty.FindAsync(id);
            if (dbKontakt == null)
                return BadRequest("Brak takiego kontaku.");

            _dbContext.Kontakty.Remove(dbKontakt);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Kontakty.ToListAsync());
        }

    }
}
