namespace EmployeeManagement.Bll.Dto
{
    public class DepartmentDto
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string Abbreviation { get; set; } = null!;

        public DateTimeOffset LastModAt { get; set; }

        public long LastModById { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public long CreatedById { get; set; }
    }
}
