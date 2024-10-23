using WebApp.Core.DTO;

namespace WebApp.Core.Interfaces.Repositories;

public interface IEmployeeRepository
{
    Task<List<EmployeeDto>> GetEmployeesById(int employeeId);

    Task EnableEmployee(int employeeId);
}
