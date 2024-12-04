using System.ComponentModel.DataAnnotations;

namespace MapDirections.Models
{

    public class Mall
    {
        [Key]
        public int MallId { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "Tên trung tâm thương mại")]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [MaxLength(500)]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Giờ mở cửa")]
        public string OpeningHours { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }

        [Required]
        [Url]
        public string Website { get; set; }

        [Required]
        [Display(Name = "Kinh độ")]
        public double Latitude { get; set; }

        [Required]
        [Display(Name = "Vĩ độ")]
        public double Longitude { get; set; }
        public ICollection<MallService>? MallServices { get; set; }
    }
}
