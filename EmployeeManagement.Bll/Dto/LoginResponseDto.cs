namespace EmployeeManagement.Bll.Dto
{
    public class LoginResponseDto
    {
        public required string Token { get; set; }

        public required long Id { get; set; }

        public required string Name { get; set; }
    }
}
