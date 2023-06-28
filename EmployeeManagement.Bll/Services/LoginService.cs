namespace EmployeeManagement.Bll.Services
{
    public class LoginService
    {
        private readonly IEmployeeService _employeeService;

        public LoginService(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
    }
}
