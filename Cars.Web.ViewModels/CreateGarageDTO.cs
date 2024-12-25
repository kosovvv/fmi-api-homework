using System.ComponentModel.DataAnnotations;

namespace Cars.Web.ViewModels
{
    public class CreateGarageDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public int Capacity { get; set; }
    }
}
