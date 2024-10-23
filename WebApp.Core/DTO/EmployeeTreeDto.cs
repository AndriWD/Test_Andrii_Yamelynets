namespace WebApp.Core.DTO;

public class EmployeeTreeDto
{
    public EmployeeTreeDto()
    {
        Employees = new List<EmployeeTreeDto>();
    }

    public int ID { get; set; }

    public string Name { get; set; }

    public int? ManagerID { get; set; }

    public List<EmployeeTreeDto> Employees { get; set; }
}
