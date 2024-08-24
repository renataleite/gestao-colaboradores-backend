namespace GestaoColaboradoresBackend.Models
{
    namespace GestaoColaboradores.Models
    {
        public class Attendance
        {
            public int Id { get; set; }
            public int CollaboratorId { get; set; }
            public DateTime Date { get; set; }
            public TimeSpan CheckInTime { get; set; } 
            public TimeSpan CheckOutTime { get; set; }

            public Collaborator Collaborator { get; set; }
        }
    }

}
