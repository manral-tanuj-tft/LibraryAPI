using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        public string Author { get; set; }

        [Range(1900, 2025)]
        public int Year { get; set; }
        public string Genre { get; set; }
    }
}
