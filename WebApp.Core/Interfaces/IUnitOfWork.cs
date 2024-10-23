using WebApp.Core.Interfaces.Repositories;

namespace WebApp.Core.Interfaces;

public interface IUnitOfWork
{
    IEmployeeRepository EmployeeRepository { get; }
}