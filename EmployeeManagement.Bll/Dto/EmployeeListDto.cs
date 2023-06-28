

namespace EmployeeManagement.Bll.Dto
{
    public class EmployeeListDto
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string Position { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public long DepartmentId { get; set; }

        public string DepartmentName { get; set; } = null!;

        public DateTimeOffset LastModAt { get; set; }

        public long LastModById { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public long CreatedById { get; set; }
    }
}
