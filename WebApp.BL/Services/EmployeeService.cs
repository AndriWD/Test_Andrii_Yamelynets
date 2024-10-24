using WebApp.Core.DTO;
using WebApp.Core.Interfaces;
using WebApp.Core.Interfaces.Services;

namespace WebApp.BL.Services
{
    public class EmployeeService : IEmployeeService
    {
        #region Private Fields

        private readonly IUnitOfWork unitOfWork;

        #endregion

        #region Constructors

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #endregion

        #region Public Methods

        public async Task<EmployeeTreeDto> GetEmployeeTreeById(int employeeId)
        {
            var employees = await this.unitOfWork.EmployeeRepository.GetEmployeesById(employeeId);

            var employeeDict = employees.ToDictionary(e => e.ID, e => new EmployeeTreeDto
            {
                ID = e.ID,
                Name = e.Name,
                ManagerID = e.ManagerID
            });

            EmployeeTreeDto rootEmployee = null;

            foreach (var employee in employees)
            {
                var employeeTreeItemDto = employeeDict[employee.ID];

                if (employee.Level == 0)
                {
                    rootEmployee = employeeTreeItemDto;
                }
                else
                {
                    var manager = employeeDict[employee.ManagerID.Value];
                    manager.Employees.Add(employeeTreeItemDto);
                }
            }

            return rootEmployee;
        }

        public async Task EnableEmployee(int employeeId)
        {
            await this.unitOfWork.EmployeeRepository.EnableEmployee(employeeId);
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
