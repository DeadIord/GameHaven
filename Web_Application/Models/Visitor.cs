using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web_Application.Models
{
    public class Visitor
    {
        [Key]
        public int VisitorCode { get; set; }
        [Required(ErrorMessage = "Укажите имя")]
        public string Name { get; set; }

        public string Addres { get; set; }
    
        public string Constant { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Visiting Visiting { get; set; }
    }
}
