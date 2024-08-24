using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GestaoColaboradoresBackend.Models
{
    public class Collaborator
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string RegistrationNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string Position { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Salary { get; set; }

        [JsonIgnore]
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}