using System.ComponentModel.DataAnnotations;

namespace AdminInterface.Models
{
    public class Job
    {
        [Key]
        public int Id { get; set; }

        [StringLength(32)]
        [Required(ErrorMessage = "Jobbtitel måste vara med!")]
        [Display(Name = "Title")]
        public string? Title { get; set; }

        [StringLength(32)]
        [Required(ErrorMessage = "Arbetsort måste vara med!")]
        [Display(Name = "Arbetsort")]
        public string? Place { get; set; }

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

        [Display(Name = "Skapat av")]
        public string? CreatedBy { get; set; } = "Okänd";

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
               ApplyFormatInEditMode = true)]
        [Display(Name = "Skapat")]
        public DateTime? CreateAt { get; set; } = DateTime.Now;
    }
}