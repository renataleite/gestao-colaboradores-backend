namespace GestaoColaboradoresBackend.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public int CollaboratorId { get; set; }

        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }

        public Collaborator Collaborator { get; set; }
    }
}
