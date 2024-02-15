using System.ComponentModel.DataAnnotations;

namespace MiniShoppingSystem.Models
{
    public class Genre
    {
        public Genre()
        {
            Books = new List<Book>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string GenreName { get; set; }
        public List<Book> Books { get; set; }
    }
}
