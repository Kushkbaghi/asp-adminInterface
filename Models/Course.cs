using System.ComponentModel.DataAnnotations;

namespace AdminInterface.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [StringLength(32)]
        [Required(ErrorMessage = "Kursenamn måste vara med!")]
        [Display(Name = "Kursnamn")]
        public string? Name { get; set; }

        [StringLength(32)]
        [Required(ErrorMessage = "Universitetsnamn måste vara med!")]
        [Display(Name = "Universitet")]
        public string? Place { get; set; }

        [StringLength(1)]
        [Required(ErrorMessage = "Prograssion måste vara med!")]
        [Display(Name = "Progression")]
        public string? Prograssion { get; set; }

        [Required(ErrorMessage = "Startdatum måste vara med!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
               ApplyFormatInEditMode = true)]
        [Display(Name = "Start datum")]
        public DateTime? Start { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Slutdatum måste vara med!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
               ApplyFormatInEditMode = true)]
        [Display(Name = "Slut datum")]
        public DateTime? End { get; set; } = DateTime.Now;

        [StringLength(32)]
        [Display(Name = "Skapat av")]
        public string? CreatedBy { get; set; } = "Okänd";

        [DataType(DataType.Date)]
        [Display(Name = "Skapat")]
        public DateTime? CreateAt { get; set; } = DateTime.Now;
    }
}