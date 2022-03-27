using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminInterface.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [StringLength(32)]
        [Required(ErrorMessage = "Projektnamn måste vara med!")]
        [Display(Name = "Projek namn")]
        public string? Name { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "Beskrivning måste vara med!")]
        [Display(Name = "Beskrivning")]
        public string? Description { get; set; }

        [StringLength(64)]
        [Required(ErrorMessage = "Projektlänk måste vara med!")]
        [Display(Name = "Länk")]
        public string? Url { get; set; }

        [StringLength(225)]
        [Display(Name = "Bildnamn")]
        public string? ImageName { get; set; }

        [StringLength(225)]
        [Display(Name = "Bild länk")]
        public string? ImageLink { get; set; } = "llll";

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        [StringLength(64)]
        [Display(Name = "Teknik")]
        public string? Tags { get; set; }

        [StringLength(32)]
        [Display(Name = "Skapat av")]
        public string? CreatedBy { get; set; } = "Okänd";

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
               ApplyFormatInEditMode = true)]
        [Display(Name = "Skapat")]
        public DateTime? CreateAt { get; set; } = DateTime.Now;
    }
}