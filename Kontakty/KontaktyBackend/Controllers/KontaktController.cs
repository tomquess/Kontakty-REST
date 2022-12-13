using KontaktyBackend.Helpers;
using KontaktyBackend.Models;
using KontaktyBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

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

           
        [HttpGet]
        public async Task<ActionResult<List<KontaktModel>>> Get()
        {

            try
            {
                return await _dbContext.Kontakty
                .Select(x => new KontaktModel()
                {
                    Id = x.Id,
                    Imie = x.Imie,
                    Nazwisko = x.Nazwisko,
                    Dataurodzenia = x.Dataurodzenia
                })
                .ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KontaktModel>> Get(int id)
        {
            var kontakt = await _dbContext.Kontakty.FindAsync(id);
            if (kontakt == null)
                return BadRequest("Brak takiego kontaku.");
            return Ok(kontakt);
        }
        // custom property dla autoryzacji
        [HttpGet("{id}/details")]
        public async Task<ActionResult<KontaktModel>> GetDetails(int id)
        {
            try
            {
                var kontakt = await _dbContext.Kontakty.FindAsync(id);
                // var kategoriaId = await _dbContext.Kategoria.FindAsync(kontakt.Kategoria.KategoriaId);
                //kontakt.Kategoria.NazwaKategorii = await _dbContext.Kategoria.
                if (kontakt == null)
                    return BadRequest("Brak takiego kontaku.");
                return Ok(kontakt);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }

        [HttpPost]
        [AuthorizeUser]  //customowy parametr do autoryzacji
        public async Task<ActionResult<List<KontaktModel>>> AddKontakt(KontaktModel kontakt)
        {
            _dbContext.Kontakty.Add(kontakt);
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.Kontakty.ToListAsync());
        }

        [HttpPut]
        [AuthorizeUser]  //customowy parametr do autoryzacji
        public async Task<ActionResult<List<KontaktModel>>> UpdateKontakt([FromForm]KontaktModel kontakt)
        {
            var dbKontakt = await _dbContext.Kontakty.FindAsync(kontakt.Id);
            if (dbKontakt == null)
                return BadRequest("Brak takiego kontaku.");

            dbKontakt.Imie = kontakt.Imie;

            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.Kontakty.ToListAsync());
        }

        [HttpDelete("{id}")]
        [AuthorizeUser]  //customowy parametr do autoryzacji
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
