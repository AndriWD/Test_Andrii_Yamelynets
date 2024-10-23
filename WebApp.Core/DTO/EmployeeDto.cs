namespace WebApp.Core.DTO;

public class EmployeeDto
{
    public int ID { get; set; }

    public string Name { get; set; }

    public int? ManagerID { get; set; }

    public int Level { get; set; }
}
