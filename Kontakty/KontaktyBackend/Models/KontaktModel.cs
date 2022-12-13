using Microsoft.Extensions.Hosting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KontaktyBackend.Models
{
    [Index(nameof(Email), IsUnique = true)] //wymaganie unikatowego adresu email
    public class KontaktModel
    {


        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Imie { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nazwisko { get; set; }

        [Required]
        [EmailAddress]
        
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Haslo { get; set; }

        [Required]
        public Kategoria Kategoria { get; set; }

        [Required]
        [Phone]
        public string Telefon { get; set; }

        [Required]
        public DateTime Dataurodzenia { get; set; }



    }

    public class Kategoria
    {
        [Key]
        public int KategoriaId { get; set; }
        [Required]
        public string NazwaKategorii { get; set; }

        public string Podkategoria { get; set; }

    }
}
