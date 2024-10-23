using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApp.Core.Interfaces.Services;
using WebApp.Models.EmployeeController.RequestModels;
using WebApp.Models.EmployeeController.ResponseModels;

namespace Test_Andrii_Yamelynets.Controllers
{
    [Route("/api/v1/employee")]
    public class EmployeeController : ApiControllerBase
    {
        #region Private Fields

        private readonly IEmployeeService employeeService;
        private readonly IMapper mapper;

        #endregion

        #region Constructors

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            this.employeeService = employeeService;
            this.mapper = mapper;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get employee by ID in tree structure.
        /// </summary>
        /// <param name="request">Employee ID.</param>
        /// <returns>Return employee info with employees tree structure.</returns>
        [HttpGet()]
        [ProducesResponseType(typeof(GetEmployeeTreeResponse), StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<IActionResult> GetEmployeeByID(GetEmployeeTreeRequest request)
        {
            var employeeTreeDto = await this.employeeService.GetEmployeeTreeById(request.EmployeeID);

            var response = this.mapper.Map<GetEmployeeTreeResponse>(employeeTreeDto);

            return new JsonResult(response);
        }

        /// <summary>
        /// Enable employee.
        /// </summary>
        /// <param name="request">Employee ID.</param>
        /// <returns>Success - NoContent.</returns>
        [HttpPatch()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<IActionResult> EnableEmployee([FromBody] EnableEmployeeRequest request)
        {
            await this.employeeService.EnableEmployee(request.EmployeeID);

            return this.NoContent();
        }

        #endregion

        #region Private Fields
        #endregion
    }
}
