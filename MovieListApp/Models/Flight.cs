using System;
using System.ComponentModel.DataAnnotations;

namespace MovieListApp.Models
{
    public class Flight
    {
        public int Id { get; set; }

        [Display(Name = "Flight Number")]
        [Required, StringLength(10)]
        public string FlightNumber { get; set; } = string.Empty;

        [Display(Name = "From")]
        [Required, StringLength(40)]
        public string FromCity { get; set; } = string.Empty;

        [Display(Name = "To")]
        [Required, StringLength(40)]
        public string ToCity { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Required]
        public DateTime Date { get; set; }

        [DataType(DataType.Currency)]
        [Range(0, 100000)]
        [Required]
        public decimal Price { get; set; }
    }
}