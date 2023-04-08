using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web_Application.Models
{
    public class Computers
    {
      
        [Key]
        public int ComputersId { get; set; }
        [Display(Name = "Название компьютера")]
        [Required(ErrorMessage = "Не указано название компьютера")]
        public string ComputerName { get; set; }
        [Display(Name = "Характеристики")]
        [Required(ErrorMessage = "Не указаны характеристики")]
        public string CompanyName { get; set; }
      
   
        public int HallsCode { get; set; }
        [Display(Name = "дата обслуживания")]
        [Required(ErrorMessage = "Не указана дата обслуживания")]
        public DateTime DateOfLastService { get; set; }
  
        public virtual Halls Halls { get; set; }

    

    }
}
