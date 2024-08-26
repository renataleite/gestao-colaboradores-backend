namespace GestaoColaboradoresBackend.Dtos
{
    public class CreateAttendanceDto
    {
        public int CollaboratorId { get; set; }
        public string CheckInTime { get; set; }
        public string CheckOutTime { get; set; }
    }
}
