using System.Data;
using System.Data.SqlClient;
using WebApp.Core.DTO;
using WebApp.Core.Interfaces.Repositories;

namespace WebApp.Data.Repositories.Employees;
public class EmployeeRepository : IEmployeeRepository
{
    private readonly string connectionString;

    public EmployeeRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public async Task<List<EmployeeDto>> GetEmployeesById(int employeeId)
    {
        var employees = new List<EmployeeDto>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = @"
                WITH EmployeeHierarchy AS (
                    SELECT 
                        ID, 
                        Name, 
                        ManagerID,
                        0 AS Level
                    FROM Employee
                    WHERE ID = @EmployeeId
                    
                    UNION ALL
                    
                    SELECT 
                        e.ID, 
                        e.Name, 
                        e.ManagerID,
                        eh.Level + 1 AS Level
                    FROM Employee e
                    INNER JOIN EmployeeHierarchy eh ON e.ManagerID = eh.ID
                )
                SELECT * FROM EmployeeHierarchy;";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = employeeId });

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                try
                {
                    var table = new DataTable();

                    connection.Open();
                    adapter.Fill(table);

                    foreach (DataRow row in table.Rows)
                    {
                        var employee = new EmployeeDto
                        {
                            ID = Convert.ToInt32(row["ID"]),
                            Name = row["Name"].ToString(),
                            ManagerID = row["ManagerID"] != DBNull.Value ? (int?)Convert.ToInt32(row["ManagerID"]) : null,
                            Level = Convert.ToInt32(row["Level"])
                        };

                        employees.Add(employee);
                    }
                }
                catch (Exception ex)
                {
                    // NOTE: need to log issue
                }
            }
        }

        return employees;
    }

    public async Task EnableEmployee(int employeeId)
    {
        try
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE Employee SET [Enable] = 1 WHERE ID = @EmployeeId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = employeeId;

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("Employee not found");
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            // NOTE: log it
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
