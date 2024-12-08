namespace MapDirections.Models
{

    public class MallViewModel
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string OpeningHours { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public List<string> Facilities { get; set; }
    }
}
