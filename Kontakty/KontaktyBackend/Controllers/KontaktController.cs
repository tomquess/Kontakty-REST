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
        public async Task<ActionResult<List<KontaktModel>>> Get()  //zwraca liste kontaktów
        {

            try
            {
                return await _dbContext.Kontakty
                .Select(x => new KontaktModel()
                {
                    Id = x.Id,
                    Imie = x.Imie,
                    Nazwisko = x.Nazwisko,
                    Dataurodzenia = x.Dataurodzenia  //zwraca podstawowe dane
                })
                .ToListAsync();  //dodaje do listy kontakt
            }
            catch (Exception ex)  //obsługa wyjątku
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KontaktModel>> Get(int id)  //zwraca kontakt ze wszystkimi danymi
        {
            var kontakt = await _dbContext.Kontakty.FindAsync(id);
            if (kontakt == null)
                return BadRequest("Brak takiego kontaku.");
            return Ok(kontakt);
        }
        // custom property dla autoryzacji
        [HttpGet("{id}/details")]
        public async Task<ActionResult<KontaktModel>> GetDetails(int id) //zwraca kontakt ze wszystkimi danymi
        {
            try
            {
                var kontakt = await _dbContext.Kontakty.FindAsync(id);  //wyszukuje kontakt w bazie danych po id
                // var kategoriaId = await _dbContext.Kategoria.FindAsync(kontakt.Kategoria.KategoriaId);
                //kontakt.Kategoria.NazwaKategorii = await _dbContext.Kategoria.
                if (kontakt == null)                                          //sprawdza obecność kontaktu w bazie danych
                    return BadRequest("Brak takiego kontaku.");
                return Ok(kontakt);
            }
            catch (Exception ex)  //obsługa wyjątków
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }

        [HttpPost]
        [AuthorizeUser]  //customowy parametr do autoryzacji
        public async Task<ActionResult<List<KontaktModel>>> AddKontakt(KontaktModel kontakt) //POST dodaje kontakt do bazy danych
        {
            _dbContext.Kontakty.Add(kontakt);
            await _dbContext.SaveChangesAsync(); //zapisz zmiany bazy danych
            return Ok(await _dbContext.Kontakty.ToListAsync()); //komunikat zwrotny
        }

        [HttpPut]
        [AuthorizeUser]  //customowy parametr do autoryzacji
        public async Task<ActionResult<List<KontaktModel>>> UpdateKontakt([FromForm]KontaktModel kontakt)   //źle zaimplementowany system aktualizacji
        {
            var dbKontakt = await _dbContext.Kontakty.FindAsync(kontakt.Id);
            if (dbKontakt == null)
                return BadRequest("Brak takiego kontaku.");

            dbKontakt.Imie = kontakt.Imie;                  //zmiana danych
            dbKontakt.Nazwisko = kontakt.Nazwisko;
            dbKontakt.Email = kontakt.Email;
            dbKontakt.Telefon = kontakt.Telefon;
            dbKontakt.Kategoria = kontakt.Kategoria;
            dbKontakt.Dataurodzenia = kontakt.Dataurodzenia;
            dbKontakt.Haslo = kontakt.Haslo;

            await _dbContext.SaveChangesAsync();        //zapis
            return Ok(await _dbContext.Kontakty.ToListAsync());
        }

        [HttpDelete("{id}")]
        [AuthorizeUser]  //customowy parametr do autoryzacji
        public async Task<ActionResult<KontaktModel>> Delete(int id)  //DELETE po id
        {
            var dbKontakt = await _dbContext.Kontakty.FindAsync(id);
            if (dbKontakt == null)
                return BadRequest("Brak takiego kontaku.");

            _dbContext.Kontakty.Remove(dbKontakt); //usunięcie
            await _dbContext.SaveChangesAsync(); //zapis

            return Ok(await _dbContext.Kontakty.ToListAsync());
        }

    }
}
