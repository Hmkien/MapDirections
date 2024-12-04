using System.ComponentModel.DataAnnotations;

namespace MapDirections.Models
{
    public class MallService
    {
        [Key]
        public int FacilityId { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Tên dịch vụ")]
        public string Name { get; set; }

        [MaxLength(200)]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Trung tâm thương mại")]
        public int? MallId { get; set; }
        [Display(Name = "Trung tâm thương mại")]
        public Mall? Mall { get; set; }
        [Display(Name = "Sự kiện")]
        public string? Event { get; set; }
    }
}