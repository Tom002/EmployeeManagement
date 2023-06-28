using EmployeeManagement.Dal.Entities.Base;

namespace EmployeeManagement.Dal.Entities
{
    public class Employee : Entity
    {
        public Employee()
        {
            Subordinates = new HashSet<Employee>();
        }

        public string Position { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public long? BossId { get; set; } 

        public virtual Employee Boss { get; set; } = null!;

        public long DepartmentId { get; set; }

        public virtual Department Department { get; set; } = null!;

        public virtual HashSet<Employee> Subordinates { get; set; }
    }
}
