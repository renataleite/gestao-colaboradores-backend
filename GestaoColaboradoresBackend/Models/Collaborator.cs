using GestaoColaboradoresBackend.Models.GestaoColaboradores.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoColaboradoresBackend.Models
{
    public class Collaborator
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }

        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}