using System;
using System.ComponentModel.DataAnnotations;

namespace Web_Application.Models
{
    public class Visiting
    {
        [Key]
        public int VisitingCode { get; set; }
        [Required(ErrorMessage = "Укажите имя")]
        public int VisitorCode { get; set; }
        [Required(ErrorMessage = "Выберите услугу")]
        public int ServicesCode { get; set; }
        [Required(ErrorMessage = "Укажите дату посещения")]
        public DateTime DateOfVisit { get; set; }
        [Required(ErrorMessage = "Укажите время посещения")]
        public DateTime VisitTime { get; set; }
        [Required(ErrorMessage = "Укажите количество часов")]
        public int NumberOfHour { get; set; }
        [Required(ErrorMessage = "Укажите зал")]
        public int HallsCode { get; set; }
        public int ComputersCode { get; set; }
        public string ApplicationUserId { get; set; }




        public Visitor Visitor { get; set; }
        public Services Services { get; set; }
        public Halls Halls { get; set; }
        public Computers Computers { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }

}
