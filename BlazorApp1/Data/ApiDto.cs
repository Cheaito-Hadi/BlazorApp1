using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Data
{
    public class ApiDto
    {
        public int ApiId { get; set; }

        [Required(ErrorMessage = "Please Api Name"), MaxLength(50)]
        public string ApiName { get; set; }
       
        public string ApiKey { get; set; }
        public bool IsUpdate { get; set; } = false;

        public DateTime CreatedDt { get; set; }
    }
}
