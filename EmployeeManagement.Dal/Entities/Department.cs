using EmployeeManagement.Dal.Entities.Base;

namespace EmployeeManagement.Dal.Entities
{
    public class Department : Entity
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
        }

        public string Abbreviation { get; set; } = null!;

        public ICollection<Employee> Employees { get; set; }
    }
}
