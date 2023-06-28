using EmployeeManagement.Dal;
using EmployeeManagement.Dal.Entities;

namespace EmployeeManagement.Api
{
    public static class SeedData
    {
        public static async Task AddSeedData(EmployeeManagementDbContext dbContext)
        {
            var marketing = new Department
            {
                Name = "Marketing",
                Abbreviation = "MARK"
            };

            var finance = new Department
            {
                Name = "Finance",
                Abbreviation = "FIN"
            };

            var hr = new Department
            {
                Name = "Human Resources",
                Abbreviation = "HR"
            };

            var it = new Department
            {
                Name = "Information Technology",
                Abbreviation = "IT"
            };

            dbContext.Departments.AddRange(marketing, finance, hr, it);

            var testPasswordHash = BCrypt.Net.BCrypt.HashPassword("password");

            dbContext.Employees.Add(new Employee
            {
                Name = "Bob",
                Username = "bob",
                PasswordHash = testPasswordHash,
                Department = marketing,
                Position = "Marketing analyst",
                Phone = "+36201234567"
            });

            dbContext.Employees.Add(new Employee
            {
                Name = "Alice",
                Username = "alice",
                PasswordHash = testPasswordHash,
                Department = finance,
                Position = "Accountant",
                Phone = "+36203214567"
            });

            dbContext.Employees.Add(new Employee
            {
                Name = "John",
                Username = "john",
                PasswordHash = testPasswordHash,
                Department = hr,
                Position = "Recruiter",
                Phone = "+36202314567"
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
