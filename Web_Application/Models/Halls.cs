using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web_Application.Models
{
    public class Halls
    {
        [Key]

        public int HallsCode { get; set; }

        [Required(ErrorMessage = "Зал не указан")]
        public string NameOfTheHall { get; set; }

        public ICollection<Visiting> Visitings { get; set; }
        public ICollection<Computers> Computers { get; set; }
    }
}
