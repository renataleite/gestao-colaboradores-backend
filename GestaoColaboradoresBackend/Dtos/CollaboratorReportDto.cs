namespace GestaoColaboradoresBackend.Dtos
{
    public class CollaboratorReportDto
    {
        public int CollaboratorId { get; set; }
        public string Name { get; set; }
        public string RegistrationNumber { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public List<AttendanceDto> Attendances { get; set; }
    }
    public class AttendanceDto
    {
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
    }
}
