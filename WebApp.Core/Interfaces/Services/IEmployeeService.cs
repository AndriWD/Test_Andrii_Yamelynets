using WebApp.Core.DTO;

namespace WebApp.Core.Interfaces.Services;

public interface IEmployeeService
{
    Task<EmployeeTreeDto> GetEmployeeTreeById(int employeeId);

    Task EnableEmployee(int employeeId);
}
