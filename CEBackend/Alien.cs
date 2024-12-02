using System.ComponentModel.DataAnnotations;

namespace CEBackend
{
    public class Alien
    {
        [Required]
        public int ID { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public Difficulty difficulty { get; set; }

        public bool PublicReveal { get; set; }

        public bool ExtraSupplies { get; set; }

    }

    public enum Difficulty
    {
        Easy,
        Medium,
        Difficult

    }
}
