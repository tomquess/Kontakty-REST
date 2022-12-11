using KontaktyBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KontaktyBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KontaktController : ControllerBase
    {
        private static List<KontaktModel> kontakty = new List<KontaktModel>
            {
                new KontaktModel {
                    Id = 1,
                    Imie = "asd",
                    Nazwisko = "sad",
                    Email = "sad",
                    Haslo = "asdasd",
                    Kategoria = "asd",
                    Podkategoria = "asd",
                    Telefon = "asd",
                    Dataurodzenia = new DateTime(2008, 5, 1, 8, 30, 52)
                },
                new KontaktModel {
                    Id = 2,
                    Imie = "sssssss",
                    Nazwisko = "saaaaaaddad",
                    Email = "sasdadsd",
                    Haslo = "asdasd",
                    Kategoria = "asssd",
                    Podkategoria = "asd",
                    Telefon = "asd",
                    Dataurodzenia = new DateTime(2008, 5, 1, 8, 30, 52)
                }
            };
        private readonly KontaktyDbContext _dbContext;

        public KontaktController(KontaktyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
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
