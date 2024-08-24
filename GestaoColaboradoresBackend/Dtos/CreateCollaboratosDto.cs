namespace GestaoColaboradoresBackend.Dtos
{
    public class CreateCollaboratorDto
    {
        public string Name { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public decimal Salary { get; set; }
    }
}
