using Microsoft.Extensions.Configuration;
using WebApp.Core.Interfaces;
using WebApp.Core.Interfaces.Repositories;
using WebApp.Data.Repositories.Employees;

namespace WebApp.Data;

public class UnitOfWork : IUnitOfWork
{
    #region Private Fields

    private readonly string connectionString;

    private IEmployeeRepository? employeeRepository;

    #endregion

    #region Constructors

    public UnitOfWork(IConfiguration configuration)
    {
        connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
    }

    #endregion

    #region Public Fields

    public IEmployeeRepository EmployeeRepository
    {
        get
        {
            if (this.employeeRepository == null)
            {
                this.employeeRepository = new EmployeeRepository(connectionString);
            }

            return this.employeeRepository;
        }
    }

    #endregion
}
