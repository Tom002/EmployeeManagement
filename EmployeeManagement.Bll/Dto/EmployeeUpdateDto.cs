namespace EmployeeManagement.Bll.Dto
{
    public class EmployeeUpdateDto
    {
        public string Name { get; set; } = null!;

        public string Position { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public int? BossId { get; set; }

        public int DepartmentId { get; set; }
    }
}
