namespace GestaoColaboradoresBackend.Dtos
{
    public class CreateAttendanceDto
    {
        public int CollaboratorId { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
    }
}
