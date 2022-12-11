namespace KontaktyBackend.Models
{
    public class KontaktModel
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; }
        public string Haslo { get; set; }
        public string Kategoria { get; set; }
        public string Podkategoria { get; set; }
        public string Telefon { get; set; }
        public DateTime Dataurodzenia { get; set; }

    }
}
