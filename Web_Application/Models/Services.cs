using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;

namespace Web_Application.Models
{
    public class Services
    {
        [Key]
        public int ServiceCode { get; set; }
        [Required(ErrorMessage = "Не указана Услуга")]
        public string NameOfTheService { get; set; }

        [Required(ErrorMessage = "Не указана цена")]
        public int PricePerhour { get; set; }

        public ICollection<Visiting> Visitings { get; set; }
    }
}